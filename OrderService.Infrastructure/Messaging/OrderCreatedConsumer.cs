using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OrderService.Application.Events;
using OrderService.Infrastructure.Data;

namespace OrderService.Infrastructure.Messaging;
public class OrderCreatedConsumer : IConsumer<OrderCreatedEvent>
{
    private readonly ILogger<OrderCreatedConsumer> _logger;
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public OrderCreatedConsumer(ILogger<OrderCreatedConsumer> logger, IServiceScopeFactory serviceScopeFactory  )
    {
        _logger = logger;
        _serviceScopeFactory = serviceScopeFactory;
    }

    public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
    {
        var message = context.Message;
        _logger.LogInformation("Received OrderCreatedEvent: OrderId={OrderId}, CustomerName={CustomerName}, TotalAmount={TotalAmount}",
            message.OrderId, message.CustomerName, message.TotalAmount);

        using var scope = _serviceScopeFactory.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<OrderDbContext>();
        var order = await db.Orders.FindAsync(message.OrderId);
        if (order is null)
        {
            _logger.LogWarning("Order {OrderId} not found", message.OrderId);
            return;
        }

        order.Status = "Processing";
        await db.SaveChangesAsync();

        _logger.LogInformation("Order {OrderId} status updated to Processing", message.OrderId);
    }
}
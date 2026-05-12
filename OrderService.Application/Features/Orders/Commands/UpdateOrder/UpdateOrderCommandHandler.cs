using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using OrderService.Domain.Interfaces;

namespace OrderService.Application.Features.Orders.Commands.UpdateOrder;

public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommandRequest>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IDistributedCache _cache;

    public UpdateOrderCommandHandler(IOrderRepository orderRepository, IDistributedCache cache)
    {
        _orderRepository = orderRepository;
        _cache = cache;
    }

    public async Task Handle(UpdateOrderCommandRequest request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetByIdAsync(request.Id)
            ?? throw new KeyNotFoundException($"Order with ID {request.Id} not found.");

        order.CustomerName = request.CustomerName;
        order.TotalAmount = request.TotalAmount;

        await _orderRepository.UpdateAsync(order);
        await _cache.RemoveAsync("all_orders", cancellationToken);
    }
}
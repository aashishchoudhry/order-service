using MassTransit;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using SharedContracts.Events;
using OrderService.Domain.Entities;
using OrderService.Domain.Interfaces;

namespace OrderService.Application.Features.Orders.Commands.CreateOrder;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommandRequest, CreateOrderResponse>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly IDistributedCache _cache;

    public CreateOrderCommandHandler(IOrderRepository orderRepository, IPublishEndpoint publishEndpoint, IDistributedCache cache)
    {
        _orderRepository = orderRepository;
        _publishEndpoint = publishEndpoint;
        _cache = cache;
    }

    public async Task<CreateOrderResponse> Handle(CreateOrderCommandRequest request, CancellationToken cancellationToken)
    {
        var order = new Order
        {
            CustomerName = request.CustomerName,
            TotalAmount = request.TotalAmount
        };

        await _orderRepository.AddAsync(order);
        await _cache.RemoveAsync("all_orders", cancellationToken);

        await _publishEndpoint.Publish(
            new OrderCreatedEvent
            {
                OrderId = order.Id,
                CustomerName = order.CustomerName,
                TotalAmount = order.TotalAmount
            },
            cancellationToken);

        return new CreateOrderResponse(order.Id);
    }
}
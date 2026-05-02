using MediatR;
using OrderService.Domain.Entities;
using OrderService.Domain.Interfaces;

namespace OrderService.Application.Features.Orders.Queries.GetOrderById;

public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQueryRequest, Order?>
{
    private readonly IOrderRepository _orderRepository;

    public GetOrderByIdQueryHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<Order?> Handle(GetOrderByIdQueryRequest request, CancellationToken cancellationToken)
    {
        return await _orderRepository.GetByIdAsync(request.Id);
    }
}

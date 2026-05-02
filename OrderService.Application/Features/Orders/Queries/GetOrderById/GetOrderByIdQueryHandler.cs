using MediatR;
using OrderService.Domain.Interfaces;

namespace OrderService.Application.Features.Orders.Queries.GetOrderById;

public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQueryRequest, GetOrderByIdResponse?>
{
    private readonly IOrderRepository _orderRepository;

    public GetOrderByIdQueryHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<GetOrderByIdResponse?> Handle(GetOrderByIdQueryRequest request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetByIdAsync(request.Id);
        if (order == null) return null;

        return new GetOrderByIdResponse(
            order.Id,
            order.CustomerName,
            order.TotalAmount,
            order.Status,
            order.CreatedAt);
    }
}

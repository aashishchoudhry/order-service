using MediatR;
using OrderService.Domain.Interfaces;

namespace OrderService.Application.Features.Orders.Queries.GetAllOrders;

public class GetAllOrdersQueryHandler : IRequestHandler<GetAllOrdersQueryRequest, IEnumerable<GetAllOrdersResponse>>
{
    private readonly IOrderRepository _repository;

    public GetAllOrdersQueryHandler(IOrderRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<GetAllOrdersResponse>> Handle(GetAllOrdersQueryRequest request, CancellationToken cancellationToken)
    {
        var orders = await _repository.GetAllAsync();
        return orders.Select(o => new GetAllOrdersResponse(
            o.Id,
            o.CustomerName,
            o.TotalAmount,
            o.Status,
            o.CreatedAt));
    }
}

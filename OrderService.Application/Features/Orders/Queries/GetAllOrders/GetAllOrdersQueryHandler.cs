using MediatR;
using OrderService.Domain.Entities;
using OrderService.Domain.Interfaces;

namespace OrderService.Application.Features.Orders.Queries.GetAllOrders;

public class GetAllOrdersQueryHandler : IRequestHandler<GetAllOrdersQueryRequest, IEnumerable<Order>>
{
    private readonly IOrderRepository _repository;

    public GetAllOrdersQueryHandler(IOrderRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Order>> Handle(GetAllOrdersQueryRequest request, CancellationToken cancellationToken)
    {
        return await _repository.GetAllAsync();
    }
}

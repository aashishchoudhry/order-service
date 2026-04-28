namespace OrderService.Application.Features.Orders.Queries.GetAllOrders;
using MediatR;
using OrderService.Domain.Entities;
using OrderService.Domain.Interfaces;

public class GetAllOrdersQueryHandler : IRequestHandler<GetAllOrdersQuery, IEnumerable<Order>>
{
    private readonly IOrderRepository _repository;

    public GetAllOrdersQueryHandler(IOrderRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Order>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAllAsync();
    }
}
using MediatR;

namespace OrderService.Application.Features.Orders.Queries.GetAllOrders;

public record GetAllOrdersQueryRequest() : IRequest<IEnumerable<GetAllOrdersResponse>>;

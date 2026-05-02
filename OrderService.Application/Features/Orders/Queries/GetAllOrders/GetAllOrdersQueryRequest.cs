using MediatR;
using OrderService.Domain.Entities;

namespace OrderService.Application.Features.Orders.Queries.GetAllOrders;

public record GetAllOrdersQueryRequest() : IRequest<IEnumerable<Order>>;

using MediatR;
using OrderService.Domain.Entities;

namespace OrderService.Application.Features.Orders.Queries.GetOrderById;

public record GetOrderByIdQueryRequest(int Id) : IRequest<Order?>;

using MediatR;
using OrderService.Domain.Entities;

namespace OrderService.Application.Features.Orders.Queries.GetOrderById;

public record GetOrderByIdQuery(int Id) : IRequest<Order?>;
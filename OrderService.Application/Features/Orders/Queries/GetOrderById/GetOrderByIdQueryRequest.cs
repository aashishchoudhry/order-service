using MediatR;

namespace OrderService.Application.Features.Orders.Queries.GetOrderById;

public record GetOrderByIdQueryRequest(int Id) : IRequest<GetOrderByIdResponse?>;

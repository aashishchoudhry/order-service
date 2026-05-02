using MediatR;

namespace OrderService.Application.Features.Orders.Commands.DeleteOrder;

public record DeleteOrderCommandRequest(int Id) : IRequest;

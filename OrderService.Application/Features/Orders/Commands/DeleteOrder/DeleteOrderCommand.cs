using MediatR;

namespace OrderService.Application.Features.Orders.Commands.DeleteOrder;

public record DeleteOrderCommand(int Id) : IRequest
{
}
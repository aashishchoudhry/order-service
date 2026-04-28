using MediatR;

namespace OrderService.Application.Features.Orders.Commands.UpdateOrder;

public record UpdateOrderCommand(int Id, string CustomerName, decimal TotalAmount) : IRequest
{
}
using MediatR;
namespace OrderService.Application.Features.Orders.Commands.CreateOrder;
public record CreateOrderCommand(string CustomerName, decimal TotalAmount) : IRequest<int>
{
}
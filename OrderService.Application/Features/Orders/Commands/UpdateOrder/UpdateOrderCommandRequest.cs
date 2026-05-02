using MediatR;

namespace OrderService.Application.Features.Orders.Commands.UpdateOrder;

public record UpdateOrderCommandRequest(int Id, string CustomerName, decimal TotalAmount) : IRequest;

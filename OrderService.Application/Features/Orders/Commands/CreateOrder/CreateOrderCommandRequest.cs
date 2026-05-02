using MediatR;

namespace OrderService.Application.Features.Orders.Commands.CreateOrder;

public record CreateOrderCommandRequest(string CustomerName, decimal TotalAmount) : IRequest<CreateOrderResponse>;

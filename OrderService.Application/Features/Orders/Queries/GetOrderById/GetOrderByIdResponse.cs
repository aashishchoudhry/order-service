namespace OrderService.Application.Features.Orders.Queries.GetOrderById;

public record GetOrderByIdResponse(
    int Id,
    string CustomerName,
    decimal TotalAmount,
    string Status,
    DateTime CreatedAt);

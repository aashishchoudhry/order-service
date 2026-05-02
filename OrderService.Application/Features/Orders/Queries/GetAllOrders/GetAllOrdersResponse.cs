namespace OrderService.Application.Features.Orders.Queries.GetAllOrders;

public record GetAllOrdersResponse(
    int Id,
    string CustomerName,
    decimal TotalAmount,
    string Status,
    DateTime CreatedAt);

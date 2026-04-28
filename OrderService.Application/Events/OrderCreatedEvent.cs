namespace OrderService.Application.Events;

public record OrderCreatedEvent(int OrderId, string CustomerName, decimal TotalAmount);
namespace OrderService.Application.DTOs;
public class UpdateOrderDto
{
    public string CustomerName { get; set; } = string.Empty;
    public decimal TotalAmount { get; set; }
}
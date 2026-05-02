using MediatR;
using OrderService.Domain.Interfaces;

namespace OrderService.Application.Features.Orders.Commands.UpdateOrder;

public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommandRequest>
{
    private readonly IOrderRepository _orderRepository;

    public UpdateOrderCommandHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task Handle(UpdateOrderCommandRequest request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetByIdAsync(request.Id)
            ?? throw new KeyNotFoundException($"Order with ID {request.Id} not found.");

        order.CustomerName = request.CustomerName;
        order.TotalAmount = request.TotalAmount;

        await _orderRepository.UpdateAsync(order);
    }
}

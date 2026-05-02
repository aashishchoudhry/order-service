using MediatR;
using OrderService.Domain.Interfaces;

namespace OrderService.Application.Features.Orders.Commands.DeleteOrder;

public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommandRequest>
{
    private readonly IOrderRepository _repository;

    public DeleteOrderCommandHandler(IOrderRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(DeleteOrderCommandRequest request, CancellationToken cancellationToken)
    {
        await _repository.DeleteAsync(request.Id);
    }
}

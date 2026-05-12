using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using OrderService.Domain.Interfaces;

namespace OrderService.Application.Features.Orders.Commands.DeleteOrder;

public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommandRequest>
{
    private readonly IOrderRepository _repository;
    private readonly IDistributedCache _cache;

    public DeleteOrderCommandHandler(IOrderRepository repository, IDistributedCache cache)
    {
        _repository = repository;
        _cache = cache;
    }

    public async Task Handle(DeleteOrderCommandRequest request, CancellationToken cancellationToken)
    {
        await _repository.DeleteAsync(request.Id);
        await _cache.RemoveAsync("all_orders", cancellationToken);
    }
}
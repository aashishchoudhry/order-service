using System.Text;
using System.Text.Json;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using OrderService.Domain.Interfaces;

namespace OrderService.Application.Features.Orders.Queries.GetAllOrders;

public class GetAllOrdersQueryHandler : IRequestHandler<GetAllOrdersQueryRequest, IEnumerable<GetAllOrdersResponse>>
{
    private const string CacheKey = "orders:all";
    private readonly IOrderRepository _repository;
    private readonly IDistributedCache _cache;

    public GetAllOrdersQueryHandler(IOrderRepository repository, IDistributedCache cache)
    {
        _repository = repository;
        _cache = cache;
    }

    public async Task<IEnumerable<GetAllOrdersResponse>> Handle(GetAllOrdersQueryRequest request, CancellationToken cancellationToken)
    {
        var cached = await _cache.GetAsync(CacheKey, cancellationToken);
        if (cached is not null)
        {
            var json = Encoding.UTF8.GetString(cached);
            return JsonSerializer.Deserialize<IEnumerable<GetAllOrdersResponse>>(json)!;
        }

        var orders = await _repository.GetAllAsync();
        var result = orders.Select(o => new GetAllOrdersResponse(
            o.Id,
            o.CustomerName,
            o.TotalAmount,
            o.Status,
            o.CreatedAt)).ToList();

        var serialized = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(result));
        await _cache.SetAsync(CacheKey, serialized, new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
        }, cancellationToken);

        return result;
    }
}

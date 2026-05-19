using System.Text;
using System.Text.Json;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using OrderService.Domain.Interfaces;

namespace OrderService.Application.Features.Orders.Queries.GetAllOrders;

public class GetAllOrdersQueryHandler : IRequestHandler<GetAllOrdersQueryRequest, IEnumerable<GetAllOrdersResponse>>
{
    private const string CacheKey = "all_orders";
    private readonly IOrderRepository _repository;
    private readonly IDistributedCache _cache;
    private readonly ILogger<GetAllOrdersQueryHandler> _logger;

    public GetAllOrdersQueryHandler(IOrderRepository repository, IDistributedCache cache, ILogger<GetAllOrdersQueryHandler> logger)
    {
        _repository = repository;
        _cache = cache;
        _logger = logger;
    }

    public async Task<IEnumerable<GetAllOrdersResponse>> Handle(GetAllOrdersQueryRequest request, CancellationToken cancellationToken)
    {
        var cached = await _cache.GetAsync(CacheKey, cancellationToken);
        if (cached is not null)
        {
            _logger.LogInformation("Cache hit for all orders");
            var json = Encoding.UTF8.GetString(cached);
            return JsonSerializer.Deserialize<IEnumerable<GetAllOrdersResponse>>(json)!;
        }
        _logger.LogInformation("Cache miss for all orders, fetching from db");
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

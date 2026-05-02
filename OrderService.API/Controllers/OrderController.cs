using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderService.Application.Features.Orders.Commands.CreateOrder;
using OrderService.Application.Features.Orders.Commands.DeleteOrder;
using OrderService.Application.Features.Orders.Commands.UpdateOrder;
using OrderService.Application.Features.Orders.Queries.GetAllOrders;
using OrderService.Application.Features.Orders.Queries.GetOrderById;

namespace OrderService.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private readonly IMediator _mediator;

    public OrderController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var orders = await _mediator.Send(new GetAllOrdersQueryRequest());
        return Ok(orders);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var order = await _mediator.Send(new GetOrderByIdQueryRequest(id));
        if (order == null) return NotFound();
        return Ok(order);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateOrderCommandRequest request)
    {
        var id = await _mediator.Send(request);
        return CreatedAtAction(nameof(GetById), new { id }, new { id });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateOrderCommandRequest request)
    {
        if (id != request.Id) return BadRequest("Route id does not match request id.");
        await _mediator.Send(request);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _mediator.Send(new DeleteOrderCommandRequest(id));
        return NoContent();
    }
}

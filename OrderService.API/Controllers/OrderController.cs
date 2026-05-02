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
    public async Task<ActionResult<IEnumerable<GetAllOrdersResponse>>> GetAll()
    {
        var response = await _mediator.Send(new GetAllOrdersQueryRequest());
        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetOrderByIdResponse>> GetById(int id)
    {
        var response = await _mediator.Send(new GetOrderByIdQueryRequest(id));
        if (response == null) return NotFound();
        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<CreateOrderResponse>> Create([FromBody] CreateOrderCommandRequest request)
    {
        var response = await _mediator.Send(request);
        return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
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

using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderService.Application.DTOs;
using OrderService.Application.Features.Orders.Commands.CreateOrder;
using OrderService.Application.Features.Orders.Commands.DeleteOrder;
using OrderService.Application.Features.Orders.Commands.UpdateOrder;
using OrderService.Application.Features.Orders.Queries.GetAllOrders;
using OrderService.Application.Features.Orders.Queries.GetOrderById;
using OrderService.Domain.Entities;
using OrderService.Domain.Interfaces;

namespace OrderService.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMediator _mediatR;

    public OrderController(IOrderRepository orderRepository, IMediator mediatR)
    {
        _orderRepository = orderRepository;
        _mediatR = mediatR;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var orders = await _mediatR.Send(new GetAllOrdersQuery());
        return Ok(orders);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var order = await _mediatR.Send(new GetOrderByIdQuery(id));
        if (order == null) return NotFound();
        return Ok(order);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateOrderDto dto)
    {
        var newOrder = new Order
        {
            CustomerName = dto.CustomerName,
            TotalAmount = dto.TotalAmount
        };
        await _mediatR.Send(new CreateOrderCommand(newOrder.CustomerName, newOrder.TotalAmount));
        return CreatedAtAction(nameof(GetById), new { id = newOrder.Id }, newOrder);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateOrderDto dto)
    {
        var order = await _mediatR.Send(new GetOrderByIdQuery(id));
        if (order == null) return NotFound();

        order.CustomerName = dto.CustomerName;
        order.TotalAmount = dto.TotalAmount;

        if (id != order.Id) return BadRequest();
        await _mediatR.Send(new UpdateOrderCommand(order.Id, order.CustomerName, order.TotalAmount));
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _mediatR.Send(new DeleteOrderCommand(id));
        return NoContent();
    }
}
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using OrderService.API.Contracts;
using OrderService.Application.Features.Auth.Commands.Login;
using OrderService.Application.Features.Auth.Commands.Register;

namespace OrderService.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        var result = await _mediator.Send(new RegisterUserCommandRequest(
            request.FirstName,
            request.LastName,
            request.Username,
            request.Password));

        if (!result.Success)
            return BadRequest(result.Error);

        return Ok("User registered successfully");
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var result = await _mediator.Send(new LoginUserCommandRequest(request.Username, request.Password));

        if (!result.Success)
            return Unauthorized(result.Error);

        return Ok(new { token = result.Token });
    }
}

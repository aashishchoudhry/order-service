using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using OrderService.Application.Common;
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
    public async Task<ActionResult<AuthResponse>> Register([FromBody] RegisterUserCommandRequest request)
    {
        var result = await _mediator.Send(request);
        if (!result.Success)
            return BadRequest(result.Error);

        return Ok(result);
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<ActionResult<AuthResponse>> Login([FromBody] LoginUserCommandRequest request)
    {
        var result = await _mediator.Send(request);
        if (!result.Success)
            return Unauthorized(result.Error);

        return Ok(result);
    }
}

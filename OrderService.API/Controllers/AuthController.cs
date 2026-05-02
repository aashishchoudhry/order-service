using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderService.API.Auth;

namespace OrderService.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly JwtTokenGenerator _tokenGenerator;

    public AuthController(JwtTokenGenerator tokenGenerator)
    {
        _tokenGenerator = tokenGenerator;
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        if (request.Username != "admin" || request.Password != "admin123")
            return Unauthorized("Invalid credentials");

        var token = _tokenGenerator.GenerateToken(request.Username, "Admin");
        return Ok(new { token });
    }
}
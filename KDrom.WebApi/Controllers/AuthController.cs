using KDrom.Application.Auth.Login;
using KDrom.Application.Auth.Register;
using KDrom.Application.Auth.Verificate;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KDrom.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginQuery request)
    {
        var result = await _mediator.Send(request);
        return Ok(result);
    }

    [HttpPost("register")]
    public async Task<ActionResult<Guid>> Register([FromBody] RegisterUserCommand request)
    {
        var userId = await _mediator.Send(request);
        return Ok(userId);
    }

    [HttpPatch("verif")]
    public async Task<ActionResult> Verificate([FromBody] VerificateUserCommand request)
    {
        await _mediator.Send(request);
        return NoContent();
    }
}

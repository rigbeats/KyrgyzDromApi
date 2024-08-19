using KDrom.Application.Auth.Login;
using KDrom.Application.Auth.Register;
using KDrom.Application.Auth.Verificate;
using KDrom.Domain.Dto;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KDrom.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[ProducesResponseType<ErrorDto>(StatusCodes.Status400BadRequest)]
[ProducesResponseType<ErrorDto>(StatusCodes.Status500InternalServerError)]
public class AuthController : ControllerBase
{
    private IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("login")]
    [ProducesResponseType<TokenVm>(StatusCodes.Status200OK)]
    public async Task<ActionResult> Login([FromBody] LoginQuery request)
    {
        var result = await _mediator.Send(request);
        return Ok(result);
    }

    [HttpPost("register")]
    [ProducesResponseType<string>(StatusCodes.Status200OK)]
    public async Task<ActionResult<string>> Register([FromBody] RegisterUserCommand request)
    {
        var userId = await _mediator.Send(request);
        return Ok(userId);
    }

    [HttpPatch("verif")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult> Verificate([FromBody] VerificateUserCommand request)
    {
        await _mediator.Send(request);
        return NoContent();
    }
}

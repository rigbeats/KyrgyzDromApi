using KDrom.Application.MediatR.Auth.Dtos;
using KDrom.Application.MediatR.Auth.Login;
using KDrom.Application.MediatR.Auth.Logout;
using KDrom.Application.MediatR.Auth.Refresh;
using KDrom.Application.MediatR.Auth.Register;
using KDrom.Application.MediatR.Auth.Verificate;
using KDrom.Domain.Dto;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KDrom.WebApi.Controllers;

/// <summary>
/// Auth controller
/// </summary>
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
    [ProducesResponseType<TokenDto>(StatusCodes.Status200OK)]
    public async Task<ActionResult<TokenDto>> Login([FromBody] LoginQuery request)
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

    [HttpPatch("verificate")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult> Verificate([FromBody] VerificateUserCommand request)
    {
        await _mediator.Send(request);
        return NoContent();
    }

    [HttpPost("refresh")]
    [ProducesResponseType<TokenDto>(StatusCodes.Status200OK)]
    public async Task<ActionResult<TokenDto>> Refresh([FromBody] RefreshTokenCommand request)
    {
        var result = await _mediator.Send(request);
        return Ok(result);
    }

    [HttpPost("logout")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult> Logout([FromBody] LogoutCommand request)
    {
        await _mediator.Send(request);
        return NoContent();
    }
}

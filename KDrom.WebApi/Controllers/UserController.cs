using KDrom.Application.Users.Queries.GetUserInfo;
using KDrom.Domain.Dto;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Library.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[ProducesResponseType<ErrorDto>(StatusCodes.Status400BadRequest)]
[ProducesResponseType<ErrorDto>(StatusCodes.Status500InternalServerError)]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id}")]
    [ProducesResponseType<UserVm>(StatusCodes.Status200OK)]
    public async Task<ActionResult<UserVm>> Get(string id)
    {
        var request = new GetUserInfoQuery(id);

        var userVm = await _mediator.Send(request);
        return Ok(userVm);
    }
}

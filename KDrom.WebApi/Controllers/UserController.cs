using KDrom.Application.Users.Queries.GetUserInfo;
using KDrom.Domain.Enums;
using KDrom.WebApi.Attribute;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
//[EnableCors("AllowAll")]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserVm>> Get(Guid id)
    {
        var request = new GetUserInfoQuery()
        {
            UserId = id
        };

        var userVm = await _mediator.Send(request);
        return Ok(userVm);
    }
}

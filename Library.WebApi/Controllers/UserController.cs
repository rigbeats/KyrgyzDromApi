using Library.Application.Models;
using Library.Application.Users.Commands.RegisterUser;
using Library.Application.Users.Queries.GetUserInfo;
using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Library.WebApi.Controllers
{
	[ApiController]
	[Route("api/users")]
	[EnableCors("AllowAll")]
	public class UserController : ControllerBase
	{
		private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

		[HttpPost("register")]
		public async Task<ActionResult<Guid>> Register([FromBody] RegisterUserCommand request)
		{
			var userId = await _mediator.Send(request);
			return Ok(userId);
		}

		[HttpGet("get/{id}")]
		public async Task<ActionResult<UserVm>> Get([FromRoute] string id)
		{
			var request = new GetUserInfoQuery()
			{
				UserId = id
			};
			var userVm = await _mediator.Send(request);
			return Ok(userVm);
		}
	}
}

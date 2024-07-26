using Library.Application.Models;
using Library.Application.Users.Commands.RegisterUser;
using Library.Application.Users.Queries.GetUserInfo;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Library.WebApi.Controllers
{
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

		[HttpPost("reg")]
		public async Task<ActionResult<Guid>> Register([FromBody] RegisterUserCommand request)
		{
			var userId = await _mediator.Send(request);
			return Ok(userId);
		}

		[HttpPatch("verif")]
		public async Task<ActionResult> Verificate()
		{
			//await
			return NoContent();
		}

		[HttpGet("get/{id}")]
		public async Task<ActionResult<UserVm>> Get(string id)
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

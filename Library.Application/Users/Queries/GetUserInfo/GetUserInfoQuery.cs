using Library.Application.Models;
using MediatR;

namespace Library.Application.Users.Queries.GetUserInfo
{
	public record GetUserInfoQuery : IRequest<UserVm>
	{
		public string UserId { get; set; }
	}
}

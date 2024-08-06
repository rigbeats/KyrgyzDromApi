using KDrom.Application.Users.Queries.GetUserInfo;
using MediatR;

namespace Library.Application.Users.Queries.GetUserInfo
{
    public record GetUserInfoQuery : IRequest<UserVm>
	{
		public string UserId { get; set; }
	}
}

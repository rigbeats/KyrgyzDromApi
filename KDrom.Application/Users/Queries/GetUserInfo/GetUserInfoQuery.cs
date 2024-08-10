using MediatR;

namespace KDrom.Application.Users.Queries.GetUserInfo;

public record GetUserInfoQuery : IRequest<UserVm>
{
    public Guid UserId { get; set; }
}

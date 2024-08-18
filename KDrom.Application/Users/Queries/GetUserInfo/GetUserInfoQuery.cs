using MediatR;

namespace KDrom.Application.Users.Queries.GetUserInfo;

public record GetUserInfoQuery(
    string UserId) : IRequest<UserVm>;
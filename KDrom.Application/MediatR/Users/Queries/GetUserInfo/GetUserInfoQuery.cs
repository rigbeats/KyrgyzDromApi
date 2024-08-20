using KDrom.Application.MediatR.Users.Dtos;
using MediatR;

namespace KDrom.Application.MediatR.Users.Queries.GetUserInfo;

public record GetUserInfoQuery(
    string UserId) : IRequest<UserDto>;
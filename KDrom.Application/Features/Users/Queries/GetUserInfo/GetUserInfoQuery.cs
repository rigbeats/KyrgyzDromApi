using KDrom.Application.Abstractions.Query;
using KDrom.Application.Common.Exceptions;
using KDrom.Application.MediatR.Users.Dtos;
using KDrom.Domain.Interfaces.IRepositories;
using Mapster;
using MediatR;

namespace KDrom.Application.MediatR.Users.Queries.GetUserInfo;

public record GetUserInfoQuery(
    string UserId) : IQuery<UserDto>;

public class GetUserInfoQueryHandler(IUserRepository userRepository) 
    : IQueryHandler<GetUserInfoQuery, UserDto>
{
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<UserDto> Handle(GetUserInfoQuery request, CancellationToken cancellationToken)
    {
        var userDb = await _userRepository.FindAsync(request.UserId)
            ?? throw new InnerException("Пользователь не найден");

        return userDb.Adapt<UserDto>();
    }
}
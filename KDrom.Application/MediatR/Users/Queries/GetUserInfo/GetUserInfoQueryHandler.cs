using KDrom.Application.Common.Exceptions;
using KDrom.Application.MediatR.Users.Dtos;
using KDrom.Domain.Interfaces.IRepositories;
using Mapster;
using MediatR;

namespace KDrom.Application.MediatR.Users.Queries.GetUserInfo;

public class GetUserInfoQueryHandler : IRequestHandler<GetUserInfoQuery, UserDto>
{
    private readonly IUserRepository _userRepository;

    public GetUserInfoQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserDto> Handle(GetUserInfoQuery request, CancellationToken cancellationToken)
    {
        var userDb = await _userRepository.GetByIdAsync(request.UserId);

        if (userDb == null)
            throw new InnerException("Пользователь не найден");

        return userDb.Adapt<UserDto>();
    }
}

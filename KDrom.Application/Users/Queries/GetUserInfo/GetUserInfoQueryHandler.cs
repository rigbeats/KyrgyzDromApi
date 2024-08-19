using KDrom.Application.Common.Exceptions;
using KDrom.Domain.Interfaces.IRepositories;
using Mapster;
using MediatR;

namespace KDrom.Application.Users.Queries.GetUserInfo;

public class GetUserInfoQueryHandler : IRequestHandler<GetUserInfoQuery, UserVm>
{
    private readonly IUserRepository _userRepository;

    public GetUserInfoQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserVm> Handle(GetUserInfoQuery request, CancellationToken cancellationToken)
    {
        var userDb = await _userRepository.GetByIdAsync(request.UserId);

        if (userDb == null)
            throw new InnerException("Пользователь не найден");

        return userDb.Adapt<UserVm>();
    }
}

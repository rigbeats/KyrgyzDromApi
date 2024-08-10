using AutoMapper;
using KDrom.Application.Common.Exceptions;
using KDrom.Domain.Interfaces.IRepositories;
using MediatR;

namespace KDrom.Application.Users.Queries.GetUserInfo;

public class GetUserInfoQueryHandler : IRequestHandler<GetUserInfoQuery, UserVm>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public GetUserInfoQueryHandler(IUserRepository userRepository,
    IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<UserVm> Handle(GetUserInfoQuery request, CancellationToken cancellationToken)
    {
        var userDb = await _userRepository.GetByIdAsync(request.UserId);

        if (userDb == null)
            throw new InnerException("Пользователь не найден");

        return _mapper.Map<UserVm>(userDb);
    }
}

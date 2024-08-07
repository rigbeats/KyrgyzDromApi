using AutoMapper;
using KDrom.Application.Users.Queries.GetUserInfo;
using KDrom.Domain.Interfaces.IRepositories;
using Library.Application.Common.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Library.Application.Users.Queries.GetUserInfo
{
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
			var userDb = _userRepository.GetByIdAsync(request.UserId);
			
			if (userDb == null)
				throw new InnerException("Пользователь не найден");

			return _mapper.Map<UserVm>(userDb);
		}
	}
}

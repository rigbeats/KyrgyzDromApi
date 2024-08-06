using AutoMapper;
using KDrom.Application.Users.Queries.GetUserInfo;
using Library.Application.Common.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Library.Application.Users.Queries.GetUserInfo
{
    public class GetUserInfoQueryHandler : IRequestHandler<GetUserInfoQuery, UserVm>
	{
		private readonly IAppDbContext _context;
		private readonly IMapper _mapper;

        public GetUserInfoQueryHandler(IAppDbContext context,
			IMapper mapper)
        {
            _context = context;
			_mapper = mapper;
        }

        public async Task<UserVm> Handle(GetUserInfoQuery request, CancellationToken cancellationToken)
		{
			var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == request.UserId);
			if (user == null)
				throw new InnerException("Пользователь не найден");

			return _mapper.Map<UserVm>(user);
		}
	}
}

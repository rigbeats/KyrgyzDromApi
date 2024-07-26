using AutoMapper;
using Library.Application.Common.Exceptions;
using Library.Application.Interfaces;
using Library.Application.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Library.Application.Users.Queries.GetUserInfo
{
	public class GetUserInfoQueryHandler : IRequestHandler<GetUserInfoQuery, UserVm>
	{
		private readonly IDromDbContext _context;
		private readonly IMapper _mapper;

        public GetUserInfoQueryHandler(IDromDbContext context,
			IMapper mapper)
        {
            _context = context;
			_mapper = mapper;
        }

        public async Task<UserVm> Handle(GetUserInfoQuery request, CancellationToken cancellationToken)
		{
			var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == request.UserId);
			if (user == null)
				throw new NotFoundException("Пользователь не найден");

			return _mapper.Map<UserVm>(user);
		}
	}
}

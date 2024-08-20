using KDrom.Application.MediatR.Makes.Dtos;
using KDrom.Domain.Interfaces.IRepositories;
using Mapster;
using MediatR;

namespace KDrom.Application.MediatR.Makes.Queries.List;

public class GetMakeListQueryHandler : IRequestHandler<GetMakeListQuery, MakeListDto>
{
    private readonly IMakeRepository _makeRepository;

    public GetMakeListQueryHandler(IMakeRepository makeRepository)
    {
        _makeRepository = makeRepository;
    }

    public async Task<MakeListDto> Handle(GetMakeListQuery request, CancellationToken cancellationToken)
    {
        var makes = await _makeRepository.GetAll();
        return makes.Adapt<MakeListDto>();
    }
}

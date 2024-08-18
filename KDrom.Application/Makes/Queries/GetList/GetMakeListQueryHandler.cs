using KDrom.Domain.Interfaces.IRepositories;
using Mapster;
using MediatR;

namespace KDrom.Application.Makes.Queries.GetList;

public class GetMakeListQueryHandler : IRequestHandler<GetMakeListQuery, MakeListVm>
{
    private readonly IMakeRepository _makeRepository;

    public GetMakeListQueryHandler(IMakeRepository makeRepository)
    {
        _makeRepository = makeRepository;
    }

    public async Task<MakeListVm> Handle(GetMakeListQuery request, CancellationToken cancellationToken)
    {
        var makes = await _makeRepository.GetAll();
        return makes.Adapt<MakeListVm>();
    }
}

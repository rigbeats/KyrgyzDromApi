using KDrom.Application.MediatR.Models.Dtos;
using KDrom.Domain.Interfaces.IRepositories;
using Mapster;
using MediatR;

namespace KDrom.Application.MediatR.Models.Queries.ListByMake;

public class GetModelListByMakeQueryHandler : IRequestHandler<GetModelListByMakeQuery, List<ModelDto>>
{
    private readonly IModelRepository _modelRepository;

    public GetModelListByMakeQueryHandler(IModelRepository modelRepository)
    {
        _modelRepository = modelRepository;
    }

    public async Task<List<ModelDto>> Handle(GetModelListByMakeQuery request, CancellationToken cancellationToken)
    {
        var models = await _modelRepository.GetAllByMakeIdAsync(request.MakeId);

        return models.Adapt<List<ModelDto>>();
    }
}

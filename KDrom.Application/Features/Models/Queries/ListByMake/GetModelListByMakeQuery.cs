using KDrom.Application.Abstractions.Query;
using KDrom.Application.MediatR.Models.Dtos;
using KDrom.Domain.Entities;
using KDrom.Domain.Interfaces.IRepositories;
using Mapster;

namespace KDrom.Application.MediatR.Models.Queries.ListByMake;

public record GetModelListByMakeQuery(
    string MakeId) : IQuery<List<ModelDto>>;

public class GetModelListByMakeQueryHandler(IRepository<Model> modelRepository) 
    : IQueryHandler<GetModelListByMakeQuery, List<ModelDto>>
{
    private readonly IRepository<Model> _modelRepository = modelRepository;

    public async Task<List<ModelDto>> Handle(GetModelListByMakeQuery request, CancellationToken cancellationToken)
        => _modelRepository
        .GetSet()
        .ProjectToType<ModelDto>()
        .ToList();
}
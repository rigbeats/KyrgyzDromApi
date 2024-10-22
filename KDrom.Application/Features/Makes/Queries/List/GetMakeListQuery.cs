using KDrom.Application.Abstractions.Query;
using KDrom.Domain.Entities;
using KDrom.Domain.Interfaces.IRepositories;
using Mapster;

namespace KDrom.Application.MediatR.Makes.Queries.List;

public record GetMakeListQuery() : IQuery<List<string>>;

public class GetMakeListQueryHandler(IRepository<Make> makeRepository) 
    : IQueryHandler<GetMakeListQuery, List<string>>
{
    private readonly IRepository<Make> _makeRepository = makeRepository;

    public async Task<List<string>> Handle(GetMakeListQuery request, CancellationToken cancellationToken)
        => _makeRepository
        .GetSet()
        .ProjectToType<string>()
        .ToList();
}
using MediatR;

namespace KDrom.Application.Makes.Queries.GetList;

public record GetMakeListQuery() : IRequest<MakeListVm>;

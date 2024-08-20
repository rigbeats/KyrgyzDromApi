using KDrom.Application.MediatR.Makes.Dtos;
using MediatR;

namespace KDrom.Application.MediatR.Makes.Queries.GetList;

public record GetMakeListQuery() : IRequest<MakeListDto>;

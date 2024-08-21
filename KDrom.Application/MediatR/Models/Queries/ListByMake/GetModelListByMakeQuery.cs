using KDrom.Application.MediatR.Models.Dtos;
using MediatR;

namespace KDrom.Application.MediatR.Models.Queries.ListByMake;

public record GetModelListByMakeQuery(
    string MakeId) : IRequest<List<ModelDto>>;

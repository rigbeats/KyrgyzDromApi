using MediatR;

namespace KDrom.Application.MediatR.Models.Commands.Create;

public record CreateModelCommand(
    string Name,
    string MakeId) : IRequest<string>;

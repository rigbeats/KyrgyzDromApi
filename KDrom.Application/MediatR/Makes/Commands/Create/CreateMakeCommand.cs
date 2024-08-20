using MediatR;

namespace KDrom.Application.MediatR.Makes.Commands.Create;

public record CreateMakeCommand(
    string Name) : IRequest<string>;
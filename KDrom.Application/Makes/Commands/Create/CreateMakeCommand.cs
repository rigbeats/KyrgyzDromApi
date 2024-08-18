using MediatR;

namespace KDrom.Application.Makes.Commands.Create;

public record CreateMakeCommand(
    string Name) : IRequest<string>;
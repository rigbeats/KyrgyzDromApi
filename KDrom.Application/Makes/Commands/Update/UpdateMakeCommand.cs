using MediatR;

namespace KDrom.Application.Makes.Commands.Update;

public record UpdateMakeCommand(
    string Id,
    string Name) : IRequest;

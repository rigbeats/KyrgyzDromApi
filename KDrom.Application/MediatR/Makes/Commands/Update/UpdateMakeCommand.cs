using MediatR;

namespace KDrom.Application.MediatR.Makes.Commands.Update;

public record UpdateMakeCommand(
    string Id,
    string Name) : IRequest;

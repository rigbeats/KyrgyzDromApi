using MediatR;

namespace KDrom.Application.MediatR.Models.Commands.Update;

public record UpdateModelCommand(
    string Id,
    string Name,
    string MakeId) : IRequest;

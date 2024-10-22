using MediatR;

namespace KDrom.Application.Abstractions.Command;

internal interface ICommandHandler<in TRequest, TResponse>
    : IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>;

internal interface ICommandHandler<in TRequest>
    : IRequestHandler<TRequest> where TRequest : IRequest;
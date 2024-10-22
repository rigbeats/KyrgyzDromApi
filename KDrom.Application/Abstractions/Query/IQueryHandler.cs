using MediatR;

namespace KDrom.Application.Abstractions.Query;

internal interface IQueryHandler<in TRequest, TResponse>
    : IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>;

internal interface IQueryHandler<in TRequest>
    : IRequestHandler<TRequest> where TRequest : IRequest;
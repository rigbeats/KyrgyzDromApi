using MediatR;

namespace KDrom.Application.Abstractions.Query;

internal interface IQuery<out TResponse> : IRequest<TResponse>;

internal interface IQuery : IRequest;

using MediatR;

namespace KDrom.Application.Abstractions.Command;

internal interface ICommand<out TResponse> : IRequest<TResponse>;

internal interface ICommand : IRequest;

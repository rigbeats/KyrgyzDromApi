using MediatR;

namespace KDrom.Application.MediatR.Models.Commands.Create;

public class CreateModelCommandHandler : IRequestHandler<CreateModelCommand, string>
{
    public Task<string> Handle(CreateModelCommand request, CancellationToken cancellationToken)
    {
        
    }
}

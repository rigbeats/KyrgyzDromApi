using KDrom.Application.Common.Exceptions;
using KDrom.Domain.Entities;
using KDrom.Domain.Interfaces.IRepositories;
using MediatR;

namespace KDrom.Application.MediatR.Makes.Commands.Create;

public class CreateMakeCommandHandler : IRequestHandler<CreateMakeCommand, string>
{
    private readonly IMakeRepository _makeRepository;

    public CreateMakeCommandHandler(IMakeRepository makeRepository)
    {
        _makeRepository = makeRepository;
    }

    public async Task<string> Handle(CreateMakeCommand request, CancellationToken cancellationToken)
    {
        if (await _makeRepository.ExistByNameAsync(request.Name))
            throw new InnerException("Марка с таким названием уже существует");

        var make = new Make()
        {
            Id = Guid.NewGuid().ToString(),
            Name = request.Name
        };

        await _makeRepository.AddAsync(make);
        await _makeRepository.SaveChangesAsync();

        return make.Id;
    }
}

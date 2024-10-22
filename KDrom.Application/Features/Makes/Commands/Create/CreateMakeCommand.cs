using KDrom.Application.Abstractions.Command;
using KDrom.Application.Common.Exceptions;
using KDrom.Domain.Entities;
using KDrom.Domain.Interfaces.IRepositories;

namespace KDrom.Application.MediatR.Makes.Commands.Create;

public record CreateMakeCommand(
    string Name) : ICommand<string>;

public class CreateMakeCommandHandler(IRepository<Make> makeRepository) 
    : ICommandHandler<CreateMakeCommand, string>
{
    private readonly IRepository<Make> _makeRepository = makeRepository;

    public async Task<string> Handle(CreateMakeCommand request, CancellationToken cancellationToken)
    {
        if (await _makeRepository.IsAnyAsync(x => x.Name == request.Name))
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
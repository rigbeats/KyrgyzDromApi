using KDrom.Application.Abstractions.Command;
using KDrom.Application.Common.Exceptions;
using KDrom.Domain.Entities;
using KDrom.Domain.Interfaces.IRepositories;
using MediatR;

namespace KDrom.Application.MediatR.Makes.Commands.Update;

public record UpdateMakeCommand(
    string Id,
    string Name) : ICommand;

public class UpdateMakeCommandHandler(IRepository<Make> makeRepository) : ICommandHandler<UpdateMakeCommand>
{
    private readonly IRepository<Make> _makeRepository = makeRepository;

    public async Task Handle(UpdateMakeCommand request, CancellationToken cancellationToken)
    {
        var makeDb = await _makeRepository.FindAsync(request.Id)
            ?? throw new InnerException("Марка не найдена");

        makeDb.Name = request.Name;

        await _makeRepository.SaveChangesAsync();
    }
}
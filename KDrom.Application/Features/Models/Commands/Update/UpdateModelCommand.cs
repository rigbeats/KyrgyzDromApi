using KDrom.Application.Abstractions.Command;
using KDrom.Application.Common.Exceptions;
using KDrom.Domain.Entities;
using KDrom.Domain.Interfaces.IRepositories;
using MediatR;

namespace KDrom.Application.MediatR.Models.Commands.Update;

public record UpdateModelCommand(
    string Id,
    string Name,
    string MakeId) : ICommand;

public class UpdateModelCommandHandler(IRepository<Model> modelRepository) 
    : ICommandHandler<UpdateModelCommand>
{
    private readonly IRepository<Model> _modelRepository = modelRepository;

    public async Task Handle(UpdateModelCommand request, CancellationToken cancellationToken)
    {
        if (await _modelRepository.IsAnyAsync(x => x.Name == request.Name && x.MakeId == request.MakeId))
            throw new InnerException("Модель с таким названием уже существует");

        var modelDb = await _modelRepository.FindAsync(request.Id)
            ?? throw new InnerException("Модель не найдена");

        modelDb.Name = request.Name;

        await _modelRepository.SaveChangesAsync();
    }
}
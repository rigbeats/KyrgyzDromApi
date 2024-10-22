using KDrom.Application.Abstractions.Command;
using KDrom.Application.Common.Exceptions;
using KDrom.Domain.Entities;
using KDrom.Domain.Interfaces.IRepositories;
using MediatR;

namespace KDrom.Application.MediatR.Models.Commands.Create;

public record CreateModelCommand(
    string Name,
    string MakeId) : ICommand<string>;

public class CreateModelCommandHandler(IRepository<Model> modelRepository) 
    : ICommandHandler<CreateModelCommand, string>
{
    private readonly IRepository<Model> _modelRepository = modelRepository;

    public async Task<string> Handle(CreateModelCommand request, CancellationToken cancellationToken)
    {
        if (await _modelRepository.IsAnyAsync(x => x.Name == request.Name && x.MakeId == request.MakeId))
            throw new InnerException("Модель с таким названием уже существует");

        var model = new Model()
        {
            Id = Guid.NewGuid().ToString(),
            MakeId = request.MakeId,
            Name = request.Name,
        };

        await _modelRepository.AddAsync(model);
        await _modelRepository.SaveChangesAsync();

        return model.Id;
    }
}
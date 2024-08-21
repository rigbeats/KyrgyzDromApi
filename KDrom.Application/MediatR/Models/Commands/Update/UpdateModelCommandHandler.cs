using KDrom.Application.Common.Exceptions;
using KDrom.Domain.Interfaces.IRepositories;
using MediatR;

namespace KDrom.Application.MediatR.Models.Commands.Update;

public class UpdateModelCommandHandler : IRequestHandler<UpdateModelCommand>
{
    private readonly IModelRepository _modelRepository;
    public UpdateModelCommandHandler(IModelRepository modelRepository)
    {
        _modelRepository = modelRepository;
    }

    public async Task Handle(UpdateModelCommand request, CancellationToken cancellationToken)
    {
        if (await _modelRepository.ExistByNameAndBrandId(request.Name, request.MakeId))
            throw new InnerException("Модель с таким названием уже существует");

        var modelDb = await _modelRepository.FindAsync(request.Id)
            ?? throw new InnerException("Модель не найдена");

        modelDb.Name = request.Name;

        await _modelRepository.SaveChangesAsync();
    }
}

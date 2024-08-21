using KDrom.Application.Common.Exceptions;
using KDrom.Domain.Entities;
using KDrom.Domain.Interfaces.IRepositories;
using MediatR;

namespace KDrom.Application.MediatR.Models.Commands.Create;

public class CreateModelCommandHandler : IRequestHandler<CreateModelCommand, string>
{
    private readonly IModelRepository _modelRepository;
    public CreateModelCommandHandler(IModelRepository modelRepository)
    {
        _modelRepository = modelRepository;
    }

    public async Task<string> Handle(CreateModelCommand request, CancellationToken cancellationToken)
    {

        if (await _modelRepository.ExistByNameAndBrandId(request.Name, request.MakeId))
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

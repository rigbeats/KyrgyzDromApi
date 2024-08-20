using KDrom.Application.Common.Exceptions;
using KDrom.Domain.Interfaces.IRepositories;
using MediatR;

namespace KDrom.Application.MediatR.Makes.Commands.Update;

public class UpdateMakeCommandHandler : IRequestHandler<UpdateMakeCommand>
{
    private readonly IMakeRepository _makeRepository;

    public UpdateMakeCommandHandler(IMakeRepository makeRepository)
    {
        _makeRepository = makeRepository;
    }

    public async Task Handle(UpdateMakeCommand request, CancellationToken cancellationToken)
    {
        var makeDb = await _makeRepository.Find(request.Id)
            ?? throw new InnerException("Марка не найдена");

        makeDb.Name = request.Name;

        await _makeRepository.SaveChangesAsync();
    }
}

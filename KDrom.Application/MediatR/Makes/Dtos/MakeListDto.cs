namespace KDrom.Application.MediatR.Makes.Dtos;

public record MakeListDto(
    IEnumerable<string> MakeNames);

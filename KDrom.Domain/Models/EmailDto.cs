namespace KDrom.Domain.Dto;

public class EmailDto
{
    public required string RecipientEmail { get; set; }

    public required string Subject { get; set; }

    public required string Message { get; set; }
}

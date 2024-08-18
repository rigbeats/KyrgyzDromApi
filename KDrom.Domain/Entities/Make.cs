namespace KDrom.Domain.Entities;

public class Make : EntityBase
{
    public string Name { get; set; }

    public ICollection<Model> Models { get; set; }
}
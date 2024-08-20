namespace KDrom.Domain.Entities;

public class Model : EntityBase
{
    public string Name { get; set; }

    public string MakeId { get; set; }

    public Make Make { get; set; }

    public ICollection<ModelGeneration> ModelGenerations { get; set; }
}
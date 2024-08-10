namespace KDrom.Domain.Entities;

public class CarModel : EntityBase
{
    public string Name { get; set; }

    public CarMake CarMake { get; set; }

    public ICollection<CarModelGeneration> CarModelGenerations { get; set; }
}
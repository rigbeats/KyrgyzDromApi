namespace KDrom.Domain.Entities;

public class CarMake : EntityBase
{
    public string Name { get; set; }

    public ICollection<CarModel> CarModels { get; set; }
}
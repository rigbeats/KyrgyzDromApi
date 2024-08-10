namespace KDrom.Domain.Entities;

public class CarModelGeneration : EntityBase
{
    public string Name { get; set; }

    public CarModel CarModel { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }
}
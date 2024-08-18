namespace KDrom.Domain.Entities;

public class AnnouncementSale : EntityBase
{
    public Make CarMake { get; set; }

    public Model CarModel { get; set; }

    public ModelGeneration CarModelGeneration { get; set; }

    public int CarReleaseYear { get; set; }

    public int Price { get; set; }

    public string Description { get; set; }

    public DateTime PublicationDateTime { get; set; }
}

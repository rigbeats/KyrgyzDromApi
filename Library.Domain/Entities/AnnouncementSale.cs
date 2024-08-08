using Library.Domain.Entities;

namespace KDrom.Domain.Entities
{
    public class AnnouncementSale : EntityBase
    {
        public CarMake CarMake { get; set; }

        public CarModel CarModel { get; set; }

        public CarModelGeneration CarModelGeneration { get; set; }

        public int CarReleaseYear { get; set; }

        public int Price { get; set; }

        public string Description { get; set; }

        public DateTime PublicationDateTime { get; set; }
    }
}

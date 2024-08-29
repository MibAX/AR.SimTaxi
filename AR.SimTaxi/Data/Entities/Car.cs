using AR.SimTaxi.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace AR.SimTaxi.Data.Entities
{
    public class Car
    {
        public int Id { get; set; } // Required 
        public string Model { get; set; }
        public string Color { get; set; }
        public string PlateNumber { get; set; }
        public DateTime ProductionDate { get; set; }
        public CarType CarType { get; set; }
        public PowerType PowerType { get; set; }

        public int? DriverId { get; set; } // ? Nullable
        public Driver? Driver { get; set; } // ? Nullable

        public List<Booking> Bookings { get; set; } = [];

        [NotMapped]
        public int Year
        {
            get
            {
                return ProductionDate.Year;
            }
        }

        [NotMapped]
        public string Info
        {
            get
            {
                return $"{Model} - {PlateNumber}";
            }
        }
    }
}

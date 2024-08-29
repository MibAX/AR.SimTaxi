using System.ComponentModel.DataAnnotations.Schema;

namespace AR.SimTaxi.Data.Entities
{
    public class Booking
    {
        public int Id { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public DateTime BookingTime { get; set; }

        [Column(TypeName = "decimal(6,2)")]
        public decimal TotalPrice { get; set; }


        public int? CarId { get; set; }
        public Car? Car { get; set; }

        public int? DriverId { get; set; }
        public Driver? Driver { get; set; }

        public List<Passenger> Passengers { get; set; } = [];
    }
}

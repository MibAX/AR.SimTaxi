using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using AR.SimTaxi.Models.Passengers;

namespace AR.SimTaxi.Models.Bookings
{
    public class BookingDetailsViewModel
    {
        [Display(Name = "Booking Number")]
        public int Id { get; set; }
        public string From { get; set; }
        public string To { get; set; }

        [Display(Name = "Booking Time")]
        public DateTime BookingTime { get; set; }

        [Column(TypeName = "decimal(6,2)")]
        [Display(Name = "Total Price")]
        public decimal TotalPrice { get; set; }

        [Display(Name = "Car")]
        public string CarInfo { get; set; }

        [Display(Name = "Driver")]
        public string DriverFullName { get; set; }

        public List<PassengerViewModel> Passengers { get; set; }
    }
}

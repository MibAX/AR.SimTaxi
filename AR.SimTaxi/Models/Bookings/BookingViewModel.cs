using AR.SimTaxi.Data.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AR.SimTaxi.Models.Bookings
{
    public class BookingViewModel
    {
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
    }
}

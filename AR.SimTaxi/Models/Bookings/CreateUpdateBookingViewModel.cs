using AR.SimTaxi.Models.Passengers;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AR.SimTaxi.Models.Bookings
{
    public class CreateUpdateBookingViewModel
    {
        public int Id { get; set; }
        public string From { get; set; }
        public string To { get; set; }

        [Display(Name = "Booking Time")]
        public DateTime BookingTime { get; set; }


        // TO DO
        // Add Car, Driver, and Passenger Lookups and Ids        
    }
}

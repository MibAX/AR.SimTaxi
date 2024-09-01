using Microsoft.AspNetCore.Mvc.Rendering;
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


        [Display(Name = "Car")]
        public int? CarId { get; set; }

        [Display(Name = "Driver")]
        public int? DriverId { get; set; }

        [Display(Name = "Passengers")]
        public List<int> PassengerIds { get; set; } = [];


        //========== Lookups ========
        public SelectList CarLookup { get; set; }
        public SelectList DriverLookup { get; set; }
        public MultiSelectList PassengerLookup { get; set; }
    }
}

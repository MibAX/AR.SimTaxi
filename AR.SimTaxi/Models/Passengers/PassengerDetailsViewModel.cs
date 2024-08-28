using AR.SimTaxi.Enums;
using System.ComponentModel.DataAnnotations;

namespace AR.SimTaxi.Models.Passengers
{
    public class PassengerDetailsViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Display(Name = "Date of Birth")]
        public DateTime? DateOfBirth { get; set; }

        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        public string? Email { get; set; }
        public Gender? Gender { get; set; }
    }
}

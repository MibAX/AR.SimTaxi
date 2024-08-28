using AR.SimTaxi.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AR.SimTaxi.Models.Passengers
{
    public class PassengerViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Full Name")]
        public string FullName { get; set; }
        public int? Age { get; set; }

        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        public string? Email { get; set; }
        public Gender? Gender { get; set; }
    }
}

using AR.SimTaxi.Enums;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace AR.SimTaxi.Models.Passengers
{
    public class CreateUpdatePassengerViewModel
    {
        public int Id { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Date of Birth")]
        public DateTime? DateOfBirth { get; set; }

        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        public string? Email { get; set; }
        public Gender? Gender { get; set; }


        // ### For Display ONLY not for creating or updating

        [ValidateNever]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }
    }
}

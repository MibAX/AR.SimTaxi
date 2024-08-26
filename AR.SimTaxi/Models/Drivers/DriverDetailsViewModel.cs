using AR.SimTaxi.Enums;
using AR.SimTaxi.Models.Cars;
using System.ComponentModel.DataAnnotations;

namespace AR.SimTaxi.Models.Drivers
{
    public class DriverDetailsViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Name")]
        public string FullName { get; set; }

        [Display(Name = "Date of Birth")]
        public DateTime DateOfBirth { get; set; }
        public Gender Gender { get; set; }

        public List<CarViewModel> Cars { get; set; } = [];
    }
}

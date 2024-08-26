using AR.SimTaxi.Enums;
using System.ComponentModel.DataAnnotations;

namespace AR.SimTaxi.Models.Drivers
{
    public class DriverViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Name")]
        public string FullName { get; set; }
        public int Age { get; set; }
        public Gender Gender { get; set; }
    }
}

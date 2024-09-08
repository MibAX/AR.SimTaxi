using AR.SimTaxi.Enums;
using System.ComponentModel.DataAnnotations;

namespace AR.SimTaxi.Models.Cars
{
    public class CarViewModel
    {
        public int Id { get; set; }

        public string Model { get; set; }
        public string Color { get; set; }

        [Display(Name = "Plate Number")]
        public string PlateNumber { get; set; }

        [Display(Name = "Car Type")]
        public CarType CarType { get; set; }

        [Display(Name = "Power Type")]
        public PowerType PowerType { get; set; }

        [Display(Name = "Driver")]
        public string DriverFullName { get; set; }
        public int Year { get; set; }

        public string Info { get; set; }
    }
}

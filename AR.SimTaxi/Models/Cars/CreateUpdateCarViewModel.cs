using AR.SimTaxi.Enums;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace AR.SimTaxi.Models.Cars
{
    public class CreateUpdateCarViewModel
    {
        public int Id { get; set; }

        public string Model { get; set; }
        public string Color { get; set; }

        [Display(Name = "Plate Number")]
        public string PlateNumber { get; set; }

        [Display(Name = "Production Date")]
        public DateTime ProductionDate { get; set; }

        [Display(Name = "Car Type")]
        public CarType CarType { get; set; }

        [Display(Name = "Power Type")]
        public PowerType PowerType { get; set; }

        [Display(Name = "Driver")]
        public int? DriverId { get; set; }

        //============= lOOKUPS

        [ValidateNever]
        public SelectList DriverLookup { get; set; }
    }
}

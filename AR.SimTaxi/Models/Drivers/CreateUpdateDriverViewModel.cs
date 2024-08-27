using AR.SimTaxi.Enums;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace AR.SimTaxi.Models.Drivers
{
    public class CreateUpdateDriverViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Gender Gender { get; set; }

        [ValidateNever]
        public string FullName { get; set; }
    }
}

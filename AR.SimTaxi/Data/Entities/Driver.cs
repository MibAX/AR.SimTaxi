using AR.SimTaxi.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace AR.SimTaxi.Data.Entities
{
    public class Driver
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Gender Gender { get; set; }

        public List<Car> Cars { get; set; } = [];


        [NotMapped]
        public string FullName
        {
            get
            {
                return $"{FirstName} {LastName}";
            }
        }

        [NotMapped]
        public int Age // 22
        {
            get
            {                    // 2024 - 2002 = 22
                return DateTime.Now.Year - DateOfBirth.Year;
            }
        }
    }
}

using AR.SimTaxi.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace AR.SimTaxi.Data.Entities
{
    public class Passenger
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string? Email { get; set; }
        public Gender? Gender { get; set; }

        public List<Booking> Bookings { get; set; } = [];

        [NotMapped]
        public string FullName
        {
            get
            {
                return $"{FirstName} {LastName}";
            }
        }

        [NotMapped]
        public int? Age 
        {
            get
            {
                if(DateOfBirth.HasValue)
                {
                    return DateTime.Now.Year - DateOfBirth.Value.Year;
                }
                else
                {
                    return null;
                }
            }
        }
    }
}

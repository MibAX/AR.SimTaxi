using AR.SimTaxi.Enums;

namespace AR.SimTaxi.Data.Entities
{
    public class Car
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
        public string PlateNumber { get; set; }
        public DateTime ProductionYear { get; set; }
        public CarType CarType { get; set; }
        public PowerType PowerType { get; set; }
    }
}

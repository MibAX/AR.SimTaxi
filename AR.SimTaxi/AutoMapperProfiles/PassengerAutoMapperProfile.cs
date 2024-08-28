using AR.SimTaxi.Data.Entities;
using AR.SimTaxi.Models.Passengers;
using AutoMapper;

namespace AR.SimTaxi.AutoMapperProfiles
{
    public class PassengerAutoMapperProfile : Profile
    {
        public PassengerAutoMapperProfile()
        {
            CreateMap<Passenger, PassengerViewModel>();
            CreateMap<Passenger, PassengerDetailsViewModel>();

            CreateMap<CreateUpdatePassengerViewModel, Passenger>().ReverseMap();
        }
    }
}

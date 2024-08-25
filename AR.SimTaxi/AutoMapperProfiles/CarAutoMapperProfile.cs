using AR.SimTaxi.Data.Entities;
using AR.SimTaxi.Models.Cars;
using AutoMapper;

namespace AR.SimTaxi.AutoMapperProfiles
{
    public class CarAutoMapperProfile : Profile
    {
        public CarAutoMapperProfile()
        {
            CreateMap<Car, CarViewModel>();
            CreateMap<Car, CarDetailsViewModel>();
        }
    }
}

using AR.SimTaxi.Data.Entities;
using AR.SimTaxi.Models.Drivers;
using AutoMapper;

namespace AR.SimTaxi.AutoMapperProfiles
{
    public class DriverAutoMapperProfile : Profile
    {
        public DriverAutoMapperProfile()
        {
            CreateMap<Driver, DriverViewModel>();
            CreateMap<Driver, DriverDetailsViewModel>();

            CreateMap<CreateUpdateDriverViewModel, Driver>();

            CreateMap<Driver, CreateUpdateDriverViewModel>();
        }
    }
}

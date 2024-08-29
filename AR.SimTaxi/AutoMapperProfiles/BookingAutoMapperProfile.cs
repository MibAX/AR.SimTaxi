using AR.SimTaxi.Data.Entities;
using AR.SimTaxi.Models.Bookings;
using AutoMapper;

namespace AR.SimTaxi.AutoMapperProfiles
{
    public class BookingAutoMapperProfile : Profile
    {
        public BookingAutoMapperProfile()
        {
            CreateMap<Booking, BookingViewModel>();
            CreateMap<Booking, BookingDetailsViewModel>();

            CreateMap<CreateUpdateBookingViewModel, Booking>().ReverseMap();
        }
    }
}

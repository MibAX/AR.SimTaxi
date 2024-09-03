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

            CreateMap<CreateUpdateBookingViewModel, Booking>();

            CreateMap<Booking, CreateUpdateBookingViewModel>()
                .ForMember(createUpdateBookingVM => createUpdateBookingVM.PassengerIds,
                    opts =>
                        opts.MapFrom(booking => booking.Passengers.Select(passenger => passenger.Id)));
        }
    }
}

using AutoMapper;
using Tennis_Court_Booking_API.Models;
using Tennis_Court_Booking_API.Models.DTO;

namespace Tennis_Court_Booking_API
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<TennisCourt, TennisCourtDto>().ReverseMap();
            CreateMap<TennisCourt, TennisCourtCreateDto>().ReverseMap();
            CreateMap<TennisCourt, TennisCourtUpdateDto>().ReverseMap();
            CreateMap<Booking, BookingDto>().ReverseMap();
            CreateMap<Booking, BookingCreateDto>().ReverseMap();
            CreateMap<Booking, BookingUpdateDto>().ReverseMap();
        }
    }
}

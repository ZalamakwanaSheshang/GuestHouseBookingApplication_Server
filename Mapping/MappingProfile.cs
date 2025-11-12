using AutoMapper;
using GuestHouseBookingApplication_Server.DTOs;
using GuestHouseBookingApplication_Server.Models;

namespace GuestHouseBookingApplication_Server.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // GuestHouse mapping
            CreateMap<GuestHouseDto, GuestHouse>()
                .ForMember(dest => dest.GuestHouseId, opt => opt.Ignore())
                .ReverseMap();

            // Room mapping
            CreateMap<RoomDto, Room>()
                .ForMember(dest => dest.RoomId, opt => opt.Ignore())
                .ReverseMap();

            // Bed mapping
            CreateMap<BedDto, Bed>()
                .ForMember(dest => dest.BedId, opt => opt.Ignore())
                .ReverseMap();

            // Booking mapping
            CreateMap<BookingDto, Booking>()
                .ForMember(dest => dest.BookingId, opt => opt.Ignore())
                .ReverseMap();

        }
    }
}

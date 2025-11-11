using AutoMapper;
using GuestHouseBookingApplication_Server.DTOs;
using GuestHouseBookingApplication_Server.Models;

namespace GuestHouseBookingApplication_Server.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // GuestHouse mappings
            //CreateMap<GuestHouseDto, GuestHouse>()
            //    .ForMember(dest => dest.GuestHouseId, opt => opt.Ignore());

            // GuestHouse mapping
            CreateMap<GuestHouseDto, GuestHouse>()
                .ForMember(dest => dest.GuestHouseId, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.ModifiedBy, opt => opt.Ignore())
                .ForMember(dest => dest.ModificationDate, opt => opt.Ignore())
                .ReverseMap();

            //CreateMap<GuestHouseDto, GuestHouse>().ReverseMap();
            // Add future mappings here
            //CreateMap<RoomDto, Room>().ReverseMap();
            //CreateMap<BedDto, Bed>().ReverseMap();
            //CreateMap<BookingDto, Booking>().ReverseMap();
        }
    }
}

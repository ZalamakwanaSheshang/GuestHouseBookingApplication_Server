using GuestHouseBookingApplication_Server.Models.Base;

namespace GuestHouseBookingApplication_Server.Models
{
    public class Room: AuditableEntity
    {
        public int RoomId { get; set; } // Room_ID
        public int GuestHouseId { get; set; } // FK
        public string RoomNo { get; set; } = null!;
        public string RoomName { get; set; } = null!;
        public string? RoomDescription { get; set; }

        // Navigation
        public GuestHouse? GuestHouse { get; set; }
        public ICollection<Bed>? Beds { get; set; }
    }
}

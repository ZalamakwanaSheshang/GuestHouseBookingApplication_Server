using GuestHouseBookingApplication_Server.Models.Base;

namespace GuestHouseBookingApplication_Server.Models
{
    public class Bed: AuditableEntity
    {
        public int BedId { get; set; } // Bed_ID
        public int RoomId { get; set; } // FK
        public string BedNo { get; set; } = null!;
        public string BedName { get; set; } = null!;
        public string? BedDescription { get; set; }

        
        // Navigation
        public Room? Room { get; set; }
    }
}

using GuestHouseBookingApplication_Server.Models.Base;

namespace GuestHouseBookingApplication_Server.Models
{
    public class GuestHouse: AuditableEntity
    {
        public int GuestHouseId { get; set; } // Guest_House_ID
        public string GuestHouseName { get; set; } = null!; // Guest_House_Name
        public string GuestHouseAddress { get; set; } = null!; // Guest_House_Address
        public string ContactEmail { get; set; } = null!; // Contact_Email
        public long? ContactNumber { get; set; } // Contact_Number


        // Relationships
        public ICollection<Room>? Rooms { get; set; }
    }
}

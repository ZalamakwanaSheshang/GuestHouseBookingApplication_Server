namespace GuestHouseBookingApplication_Server.Models
{
    public class GuestHouse
    {
        public int GuestHouseId { get; set; } // Guest_House_ID
        public string GuestHouseName { get; set; } = null!; // Guest_House_Name
        public string GuestHouseAddress { get; set; } = null!; // Guest_House_Address
        public string ContactEmail { get; set; } = null!; // Contact_Email
        public long? ContactNumber { get; set; } // Contact_Number

        public DateTime? CreatedDate { get; set; } // Created_Date
        public int? CreatedBy { get; set; } // Created_By
        public DateTime? ModificationDate { get; set; } // Modification_Date
        public int? ModifiedBy { get; set; } // Modified_By
        public string ActiveStatus { get; set; } = "Active"; // Active_Status

        // Relationships
        public ICollection<Room>? Rooms { get; set; }
    }
}

namespace GuestHouseBookingApplication_Server.Models
{
    public class Bed
    {
        public int BedId { get; set; } // Bed_ID
        public int RoomId { get; set; } // FK
        public string BedNo { get; set; } = null!;
        public string BedName { get; set; } = null!;
        public string? BedDescription { get; set; }

        // Audit Columns
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ActiveStatus { get; set; } = "Active";

        // Navigation
        public Room? Room { get; set; }
    }
}

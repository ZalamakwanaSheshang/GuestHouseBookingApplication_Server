namespace GuestHouseBookingApplication_Server.Models
{
    public class Room
    {
        public int RoomId { get; set; } // Room_ID
        public int GuestHouseId { get; set; } // FK
        public string RoomNo { get; set; } = null!;
        public string RoomName { get; set; } = null!;
        public string? RoomDescription { get; set; }

        // Audit Columns
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ActiveStatus { get; set; } = "Active";

        // Navigation
        public GuestHouse? GuestHouse { get; set; }
        public ICollection<Bed>? Beds { get; set; }
    }
}

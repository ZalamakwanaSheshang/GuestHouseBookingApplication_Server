namespace GuestHouseBookingApplication_Server.Models
{
    public class Booking
    {
        public int BookingId { get; set; } // Booking_ID
        public int UserId { get; set; } // FK
        public int BedId { get; set; } // FK
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public string BookingStatus { get; set; } = "Booked";

        // Audit Columns
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ActiveStatus { get; set; } = "Active";

        // Navigation
        public User? User { get; set; }
        public Bed? Bed { get; set; }
    }
}

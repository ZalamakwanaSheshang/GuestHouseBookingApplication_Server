using GuestHouseBookingApplication_Server.Models.Base;

namespace GuestHouseBookingApplication_Server.Models
{
    public class Booking: AuditableEntity
    {
        public int BookingId { get; set; } // Booking_ID
        public int UserId { get; set; } // FK
        public int BedId { get; set; } // FK
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public string BookingStatus { get; set; } = "Pending"; // Pending, Approved, Rejected, Cancelled
        public string? BookingReason { get; set; } // entered by user
        public string? AdminRemarks { get; set; } // entered by admin



        // Navigation
        public User? User { get; set; }
        public Bed? Bed { get; set; }
    }
}

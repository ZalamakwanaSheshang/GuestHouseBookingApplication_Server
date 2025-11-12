namespace GuestHouseBookingApplication_Server.DTOs
{
    public class BookingDto
    {
        public int BookingId { get; set; }
        public int UserId { get; set; }
        public int BedId { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public string BookingStatus { get; set; } = "Pending";
        public string BookingReason { get; set; } = null!;
        public string? AdminRemarks { get; set; }
        public string ActiveStatus { get; set; } = "Active";
    }
}

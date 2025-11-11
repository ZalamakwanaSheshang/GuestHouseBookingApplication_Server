namespace GuestHouseBookingApplication_Server.DTOs
{
    public class GuestHouseDto
    {
        public int GuestHouseId { get; set; }
        public string GuestHouseName { get; set; } = null!;
        public string GuestHouseAddress { get; set; } = null!;
        public string ContactEmail { get; set; } = null!;
        public long? ContactNumber { get; set; }
        public string ActiveStatus { get; set; } = "Active";
    }
}

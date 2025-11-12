namespace GuestHouseBookingApplication_Server.DTOs
{
    public class BedDto
    {
        public int BedId { get; set; }
        public int RoomId { get; set; }
        public string BedNo { get; set; } = null!;
        public string BedName { get; set; } = null!;
        public string? BedDescription { get; set; }
        public string ActiveStatus { get; set; } = "Active";
    }
}

namespace GuestHouseBookingApplication_Server.DTOs
{
    public class RoomDto
    {
        public int RoomId { get; set; }
        public int GuestHouseId { get; set; }
        public string RoomNo { get; set; } = null!;
        public string RoomName { get; set; } = null!;
        public string? RoomDescription { get; set; }
        public string ActiveStatus { get; set; } = "Active";
    }
}

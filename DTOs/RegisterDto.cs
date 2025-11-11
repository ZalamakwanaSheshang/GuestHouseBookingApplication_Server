namespace GuestHouseBookingApplication_Server.DTOs
{
    public class RegisterDto
    {
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string EmailId { get; set; } = null!;
        public long? ContactNumber { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Role { get; set; } = "User";
    }
}

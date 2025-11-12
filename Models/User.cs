using System;
using GuestHouseBookingApplication_Server.Models.Base;

namespace GuestHouseBookingApplication_Server.Models
{
    public class User: AuditableEntity
    {
        public int UserId { get; set; } // maps to User_ID
        public string Username { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public string PasswordSalt { get; set; } = null!;
        public string EmailId { get; set; } = null!;
        public long? ContactNumber { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Role { get; set; } = "User"; // 'Admin','User','Guest'
    }
}

using System;

namespace GuestHouseBookingApplication_Server.Models
{
    public class User
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

        //Audit Columns
        public int? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public int? ModifiedBy { get; set; }
        public DateTime? ModificationDate { get; set; }
        public string? ActiveStatus { get; set; } = "Active"; // 'Active','Closed'
    }
}

using GuestHouseBookingApplication_Server.Data;
using GuestHouseBookingApplication_Server.Models;
using GuestHouseBookingApplication_Server.Security;

namespace GuestHouseBookingApplication_Server.Data
{
    public class SeedData
    {
        public static async Task InitializeAsync(AppDbContext context)
        {
            if (!context.Users.Any(u => u.Role == "Admin"))
            {
                var (hash, salt) = PasswordHasher.HashPassword("Admin@123");

                var admin = new User
                {
                    Username = "admin",
                    PasswordHash = hash,
                    PasswordSalt = salt,
                    EmailId = "admin@guesthouse.com",
                    FirstName = "System",
                    LastName = "Administrator",
                    Role = "Admin",
                    CreatedDate = DateTime.UtcNow,
                    ActiveStatus = "Active",
                };

                context.Users.Add(admin);
                await context.SaveChangesAsync();
            }
        }
    }
}

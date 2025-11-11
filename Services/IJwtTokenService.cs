using GuestHouseBookingApplication_Server.Models;

namespace GuestHouseBookingApplication_Server.Services
{
    public interface IJwtTokenService
    {
        string GenerateToken(User user);
    }
}

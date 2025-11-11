using GuestHouseBookingApplication_Server.DTOs;
using GuestHouseBookingApplication_Server.Repositories;
using GuestHouseBookingApplication_Server.Security;
using GuestHouseBookingApplication_Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace GuestHouseBookingApplication_Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly IJwtTokenService _jwt;

        public AuthController(IUnitOfWork uow, IJwtTokenService jwt)
        {
            _uow = uow;
            _jwt = jwt;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            var existing = await _uow.Users.FindAsync(u => u.Username == dto.Username);
            if (existing.Any())
                return BadRequest("Username already exists");

            var (hash, salt) = PasswordHasher.HashPassword(dto.Password);
            var user = new GuestHouseBookingApplication_Server.Models.User
            {
                Username = dto.Username,
                PasswordHash = hash,
                PasswordSalt = salt,
                EmailId = dto.EmailId,
                ContactNumber = dto.ContactNumber,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Role = dto.Role,
                CreatedDate = DateTime.UtcNow,
                ActiveStatus = "Active",
            };

            await _uow.Users.AddAsync(user);
            await _uow.CommitAsync();

            return Ok(new { message = "Registered" });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var users = await _uow.Users.FindAsync(u => u.Username == dto.Username);
            var user = users.FirstOrDefault();
            if (user == null)
                return Unauthorized("Invalid credentials");

            bool ok = PasswordHasher.Verify(dto.Password, user.PasswordHash, user.PasswordSalt);
            if (!ok)
                return Unauthorized("Invalid credentials");

            var token = _jwt.GenerateToken(user);
            return Ok(new { token });
        }
    }
}

using System.Security.Claims;
using AutoMapper;
using GuestHouseBookingApplication_Server.DTOs;
using GuestHouseBookingApplication_Server.Models;
using GuestHouseBookingApplication_Server.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GuestHouseBookingApplication_Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // both Admin & User can access
    public class BookingController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public BookingController(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        // Get all bookings (Admin only)
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            var bookings = await _uow.Bookings.GetAllAsync();
            var result = _mapper.Map<IEnumerable<BookingDto>>(bookings);
            return Ok(result);
        }

        // Get bookings for current user
        [HttpGet("my")]
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> GetMyBookings()
        {
            var currentUserId = GetCurrentUserId();
            if (currentUserId == null)
                return Unauthorized();

            var bookings = await _uow.Bookings.FindAsync(b => b.UserId == currentUserId);
            var result = _mapper.Map<IEnumerable<BookingDto>>(bookings);
            return Ok(result);
        }

        // Get booking by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var booking = await _uow.Bookings.GetByIdAsync(id);
            if (booking == null)
                return NotFound($"Booking with ID {id} not found.");

            var result = _mapper.Map<BookingDto>(booking);
            return Ok(result);
        }

        // Create booking (checks bed availability)
        [HttpPost]
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> Create([FromBody] BookingDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (string.IsNullOrWhiteSpace(dto.BookingReason))
                return BadRequest("Booking reason is required.");

            // Validate bed availability
            bool isAvailable = await IsBedAvailable(dto.BedId, dto.CheckInDate, dto.CheckOutDate);
            if (!isAvailable)
                return BadRequest("The selected bed is already booked for these dates.");

            // Get current user (for normal users)
            var currentUserId = GetCurrentUserId();
            if (User.IsInRole("User"))
                dto.UserId = currentUserId ?? dto.UserId;

            var entity = _mapper.Map<Booking>(dto);
            entity.BookingStatus = "Pending";
            entity.AdminRemarks = null;

            await _uow.Bookings.AddAsync(entity);
            await _uow.CommitAsync();

            var result = _mapper.Map<BookingDto>(entity);
            return Ok(new { message = "Booking request submitted successfully (Pending approval)", data = result });
        }

        // Update booking (Admin only)
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, [FromBody] BookingDto dto)
        {
            var existing = await _uow.Bookings.GetByIdAsync(id);
            if (existing == null)
                return NotFound($"Booking with ID {id} not found.");

            // Validate bed availability (if bed/dates changed)
            if (
                existing.BedId != dto.BedId
                || existing.CheckInDate != dto.CheckInDate
                || existing.CheckOutDate != dto.CheckOutDate
            )
            {
                bool isAvailable = await IsBedAvailable(
                    dto.BedId,
                    dto.CheckInDate,
                    dto.CheckOutDate,
                    id
                );
                if (!isAvailable)
                    return BadRequest("The selected bed is already booked for these dates.");
            }

            _mapper.Map(dto, existing);
            _uow.Bookings.Update(existing);
            await _uow.CommitAsync();

            return Ok(new { message = "Booking updated successfully" });
        }

        // Approve or Reject a Booking (Admin only)
        [HttpPut("{id}/review")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ReviewBooking(int id, [FromBody] BookingDto dto)
        {
            var booking = await _uow.Bookings.GetByIdAsync(id);
            if (booking == null)
                return NotFound($"Booking with ID {id} not found.");

            if (dto.BookingStatus != "Approved" && dto.BookingStatus != "Rejected")
                return BadRequest("BookingStatus must be either 'Approved' or 'Rejected'.");

            booking.BookingStatus = dto.BookingStatus;
            booking.AdminRemarks = dto.AdminRemarks ?? $"Booking {dto.BookingStatus.ToLower()} by admin.";
            _uow.Bookings.Update(booking);
            await _uow.CommitAsync();

            return Ok(new
            {
                message = $"Booking {dto.BookingStatus.ToLower()} successfully",
                data = _mapper.Map<BookingDto>(booking)
            });
        }

        // Cancel (soft delete) booking
        [HttpDelete("{id}")]
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> Cancel(int id)
        {
            var booking = await _uow.Bookings.GetByIdAsync(id);
            if (booking == null)
                return NotFound($"Booking with ID {id} not found.");

            // Restrict users to cancel only their own bookings
            var currentUserId = GetCurrentUserId();
            if (User.IsInRole("User") && booking.UserId != currentUserId)
                return Forbid();

            booking.BookingStatus = "Cancelled";
            _uow.Bookings.SoftDelete(booking);
            await _uow.CommitAsync();

            return Ok(new { message = "Booking cancelled successfully" });
        }

        // =================== Helper Methods ===================

        private int? GetCurrentUserId()
        {
            var userIdClaim = User.FindFirst("userId")?.Value;
            if (int.TryParse(userIdClaim, out int userId))
                return userId;
            return null;
        }

        private async Task<bool> IsBedAvailable(
            int bedId,
            DateTime checkIn,
            DateTime checkOut,
            int? excludeBookingId = null
        )
        {
            var bookings = await _uow.Bookings.FindAsync(b =>
                b.BedId == bedId
                && b.ActiveStatus == "Active"
                && b.BookingStatus == "Booked"
                && (excludeBookingId == null || b.BookingId != excludeBookingId)
                && (
                    (checkIn >= b.CheckInDate && checkIn < b.CheckOutDate)
                    || // check-in overlaps
                    (checkOut > b.CheckInDate && checkOut <= b.CheckOutDate)
                    || // check-out overlaps
                    (checkIn <= b.CheckInDate && checkOut >= b.CheckOutDate) // fully overlaps
                )
            );

            return !bookings.Any();
        }
    }
}

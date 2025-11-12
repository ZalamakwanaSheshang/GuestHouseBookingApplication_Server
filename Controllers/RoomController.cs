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
    [Authorize(Roles = "Admin")]
    public class RoomController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public RoomController(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _uow.Rooms.GetAllAsync();
            var result = _mapper.Map<IEnumerable<RoomDto>>(data);
            return Ok(result);
        }

        [HttpGet("byGuestHouse/{guestHouseId}")]
        public async Task<IActionResult> GetByGuestHouse(int guestHouseId)
        {
            var rooms = await _uow.Rooms.FindAsync(r => r.GuestHouseId == guestHouseId);
            var result = _mapper.Map<IEnumerable<RoomDto>>(rooms);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var entity = await _uow.Rooms.GetByIdAsync(id);
            if (entity == null)
                return NotFound($"Room with ID {id} not found.");

            var dto = _mapper.Map<RoomDto>(entity);
            return Ok(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Create(RoomDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var entity = _mapper.Map<Room>(dto);
            await _uow.Rooms.AddAsync(entity);
            await _uow.CommitAsync();

            var result = _mapper.Map<RoomDto>(entity);
            return Ok(new { message = "Room created successfully", data = result });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, RoomDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existing = await _uow.Rooms.GetByIdAsync(id);
            if (existing == null)
                return NotFound($"Room with ID {id} not found.");

            _mapper.Map(dto, existing);
            _uow.Rooms.Update(existing);
            await _uow.CommitAsync();

            var result = _mapper.Map<RoomDto>(existing);
            return Ok(new { message = "Room updated successfully", data = result });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _uow.Rooms.GetByIdAsync(id);
            if (entity == null)
                return NotFound($"Room with ID {id} not found.");

            _uow.Rooms.SoftDelete(entity);
            await _uow.CommitAsync();
            return Ok(new { message = "Room deleted successfully" });
        }
    }
}

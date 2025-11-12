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
    public class BedController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public BedController(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var beds = await _uow.Beds.GetAllAsync();
            var result = _mapper.Map<IEnumerable<BedDto>>(beds);
            return Ok(result);
        }

        [HttpGet("byRoom/{roomId}")]
        public async Task<IActionResult> GetByRoom(int roomId)
        {
            var beds = await _uow.Beds.FindAsync(b => b.RoomId == roomId);
            var result = _mapper.Map<IEnumerable<BedDto>>(beds);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var entity = await _uow.Beds.GetByIdAsync(id);
            if (entity == null)
                return NotFound($"Bed with ID {id} not found.");

            var dto = _mapper.Map<BedDto>(entity);
            return Ok(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Create(BedDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var entity = _mapper.Map<Bed>(dto);
            await _uow.Beds.AddAsync(entity);
            await _uow.CommitAsync();

            var result = _mapper.Map<BedDto>(entity);
            return Ok(new { message = "Room created successfully", data = result });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, BedDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existing = await _uow.Beds.GetByIdAsync(id);
            if (existing == null)
                return NotFound($"Bed with ID {id} not found.");

            _mapper.Map(dto, existing);
            _uow.Beds.Update(existing);
            await _uow.CommitAsync();

            var result = _mapper.Map<BedDto>(existing);
            return Ok(new { message = "Room updated successfully", data = result });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _uow.Beds.GetByIdAsync(id);
            if (entity == null)
                return NotFound($"Bed with ID {id} not found.");

            _uow.Beds.SoftDelete(entity);
            await _uow.CommitAsync();
            return Ok(new { message = "Room deleted successfully" });
        }
    }
}

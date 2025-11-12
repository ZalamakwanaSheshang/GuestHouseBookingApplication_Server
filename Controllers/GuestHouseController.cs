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
    public class GuestHouseController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public GuestHouseController(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        // Get all guest houses
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _uow.GuestHouses.GetAllAsync();
            var result = _mapper.Map<IEnumerable<GuestHouseDto>>(data);
            return Ok(result);
        }

        // Get a single guest house by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var entity = await _uow.GuestHouses.GetByIdAsync(id);
            if (entity == null)
                return NotFound($"Guest House with ID {id} not found.");

            var dto = _mapper.Map<GuestHouseDto>(entity);
            return Ok(dto);
        }

        // Create a new guest house
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] GuestHouseDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // AutoMapper handles field mapping
            var entity = _mapper.Map<GuestHouse>(dto);

            await _uow.GuestHouses.AddAsync(entity);
            await _uow.CommitAsync();

            var result = _mapper.Map<GuestHouseDto>(entity);
            return Ok(new { message = "Guest house created successfully", data = result });
        }

        // Update an existing guest house
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] GuestHouseDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existing = await _uow.GuestHouses.GetByIdAsync(id);
            if (existing == null)
                return NotFound($"Guest house with ID {id} not found.");

            _mapper.Map(dto, existing); // applies updates automatically
            _uow.GuestHouses.Update(existing);
            await _uow.CommitAsync();

            var result = _mapper.Map<GuestHouseDto>(existing);
            return Ok(new { message = "Guest house updated successfully", data = result });
        }

        // Delete a guest house
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    var entity = await _uow.GuestHouses.GetByIdAsync(id);
        //    if (entity == null)
        //        return NotFound($"Guest house with ID {id} not found.");

        //    _uow.GuestHouses.Remove(entity);
        //    await _uow.CommitAsync();

        //    return Ok(new { message = "Guest house deleted successfully" });
        //}

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _uow.GuestHouses.GetByIdAsync(id);
            if (entity == null)
                return NotFound($"Guest house with ID {id} not found.");

            _uow.GuestHouses.SoftDelete(entity);
            await _uow.CommitAsync();

            return Ok(new { message = "Guest house deleted successfully" });
        }

    }
}

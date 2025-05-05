using EducaERP.Application.DTOs.Academics;
using EducaERP.Application.Interfaces.Academics;
using EducaERP.Core.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EducaERP.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ApiExplorerSettings(GroupName = "Acadêmico")]
    public class AttendancesController : ControllerBase
    {
        private readonly IAttendanceService _service;

        public AttendancesController(IAttendanceService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var attendances = await _service.GetAll();
            return Ok(attendances);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var attendance = await _service.GetById(id);
                return Ok(attendance);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("by-student/{studentId}")]
        public async Task<IActionResult> GetByStudent(Guid studentId)
        {
            var attendances = await _service.GetByStudent(studentId);
            return Ok(attendances);
        }

        [HttpGet("by-discipline/{disciplineId}")]
        public async Task<IActionResult> GetByDiscipline(Guid disciplineId)
        {
            var attendances = await _service.GetByDiscipline(disciplineId);
            return Ok(attendances);
        }

        [HttpGet("by-date-range")]
        public async Task<IActionResult> GetByDateRange([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            var attendances = await _service.GetByDateRange(startDate, endDate);
            return Ok(attendances);
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] AttendanceDTO dto)
        {
            try
            {
                var attendance = await _service.Register(dto);
                return CreatedAtAction(nameof(GetById), new { id = attendance.Id }, attendance);
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] AttendanceDTO dto)
        {
            try
            {
                var attendance = await _service.Update(id, dto);
                return Ok(attendance);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _service.Delete(id);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
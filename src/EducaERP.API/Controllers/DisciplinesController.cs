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
    [Route("api/disciplina")]
    [ApiExplorerSettings(GroupName = "Acadêmico")]
    public class DisciplinesController : ControllerBase
    {
        private readonly IDisciplineService _service;

        public DisciplinesController(IDisciplineService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] bool? activeOnly = true)
        {
            var disciplines = await _service.GetAll(activeOnly);
            return Ok(disciplines);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var discipline = await _service.GetById(id);
                return Ok(discipline);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("by-course/{courseId}")]
        public async Task<IActionResult> GetByCourse(Guid courseId)
        {
            var disciplines = await _service.GetByCourse(courseId);
            return Ok(disciplines);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] DisciplineDTO dto)
        {
            try
            {
                var discipline = await _service.Create(dto);
                return CreatedAtAction(nameof(GetById), new { id = discipline.Id }, discipline);
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] DisciplineDTO dto)
        {
            try
            {
                var discipline = await _service.Update(id, dto);
                return Ok(discipline);
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

        [HttpPatch("{id}/toggle-status")]
        public async Task<IActionResult> ToggleStatus(Guid id)
        {
            try
            {
                await _service.ToggleStatus(id);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
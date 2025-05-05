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
    public class GradesController : ControllerBase
    {
        private readonly IGradeService _service;

        public GradesController(IGradeService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var grades = await _service.GetAll();
            return Ok(grades);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var grade = await _service.GetById(id);
                return Ok(grade);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("by-student/{studentId}")]
        public async Task<IActionResult> GetByStudent(Guid studentId)
        {
            var grades = await _service.GetByStudent(studentId);
            return Ok(grades);
        }

        [HttpGet("by-discipline/{disciplineId}")]
        public async Task<IActionResult> GetByDiscipline(Guid disciplineId)
        {
            var grades = await _service.GetByDiscipline(disciplineId);
            return Ok(grades);
        }

        [HttpGet("average/{studentId}/{disciplineId}")]
        public async Task<IActionResult> CalculateAverage(Guid studentId, Guid disciplineId)
        {
            var average = await _service.CalculateAverage(studentId, disciplineId);
            return Ok(new { Average = average });
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] GradeDTO dto)
        {
            try
            {
                var grade = await _service.Register(dto);
                return CreatedAtAction(nameof(GetById), new { id = grade.Id }, grade);
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
        public async Task<IActionResult> Update(Guid id, [FromBody] GradeDTO dto)
        {
            try
            {
                var grade = await _service.Update(id, dto);
                return Ok(grade);
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
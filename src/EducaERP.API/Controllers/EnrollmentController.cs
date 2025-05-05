using EducaERP.Application.DTOs.Enrollments;
using EducaERP.Application.DTOs.Enrollments.Responses;
using EducaERP.Application.Interfaces.Enrollments;
using EducaERP.Core.Enums;
using EducaERP.Core.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EducaERP.API.Controllers
{
    [ApiController]
    [Route("api/matriculas")]
    [ApiExplorerSettings(GroupName = "Matrícula")]
    public class EnrollmentsController : ControllerBase
    {
        private readonly IEnrollmentService _service;

        public EnrollmentsController(IEnrollmentService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] EnrollmentDTO dto)
        {
            try
            {
                var result = await _service.Create(dto);
                return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var result = await _service.GetById(id);
                return Ok(result);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAll();
            return Ok(result);
        }

        [HttpGet("por-aluno/{alunoId}")]
        public async Task<IActionResult> GetByStudent(Guid alunoId)
        {
            try
            {
                var result = await _service.GetByStudent(alunoId);
                return Ok(result);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("por-curso/{cursoId}")]
        public async Task<IActionResult> GetByCourse(Guid cursoId)
        {
            try
            {
                var result = await _service.GetByCourse(cursoId);
                return Ok(result);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("por-ano/{anoLetivo}")]
        public async Task<IActionResult> GetByYear(string anoLetivo)
        {
            var result = await _service.GetByYear(anoLetivo);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] EnrollmentDTO dto)
        {
            try
            {
                var result = await _service.Update(id, dto);
                return Ok(result);
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

        [HttpPatch("{id}/status")]
        public async Task<IActionResult> ChangeStatus(Guid id, [FromBody] EnrollmentStatus newStatus)
        {
            try
            {
                await _service.ChangeStatus(id, newStatus);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
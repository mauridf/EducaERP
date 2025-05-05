using EducaERP.Application.DTOs.Students;
using EducaERP.Application.Interfaces.Students;
using EducaERP.Core.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EducaERP.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ApiExplorerSettings(GroupName = "Alunos")]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _service;

        public StudentsController(IStudentService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var students = await _service.GetAll();
            return Ok(students);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var student = await _service.GetById(id);
                return Ok(student);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("by-institution/{institutionId}")]
        public async Task<IActionResult> GetByInstitution(Guid institutionId)
        {
            var students = await _service.GetByInstitution(institutionId);
            return Ok(students);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] StudentDTO dto)
        {
            try
            {
                var student = await _service.Create(dto);
                return CreatedAtAction(nameof(GetById), new { id = student.Id }, student);
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
        public async Task<IActionResult> Update(Guid id, [FromBody] StudentDTO dto)
        {
            try
            {
                var student = await _service.Update(id, dto);
                return Ok(student);
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
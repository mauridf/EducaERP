using EducaERP.Application.DTOs.Teachers;
using EducaERP.Application.Interfaces.Teachers;
using EducaERP.Core.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace EducaERP.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ApiExplorerSettings(GroupName = "Professores")]
    public class TeachersController : ControllerBase
    {
        private readonly ITeacherService _service;

        public TeachersController(ITeacherService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var teachers = await _service.GetAll();
            return Ok(teachers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var teacher = await _service.GetById(id);
                return Ok(teacher);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("by-institution/{institutionId}")]
        public async Task<IActionResult> GetByInstitution(Guid institutionId)
        {
            var teachers = await _service.GetByInstitution(institutionId);
            return Ok(teachers);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TeacherDTO dto)
        {
            try
            {
                var teacher = await _service.Create(dto);
                return CreatedAtAction(nameof(GetById), new { id = teacher.Id }, teacher);
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
        public async Task<IActionResult> Update(Guid id, [FromBody] TeacherDTO dto)
        {
            try
            {
                var teacher = await _service.Update(id, dto);
                return Ok(teacher);
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

        [HttpPost("add-discipline")]
        public async Task<IActionResult> AddDiscipline([FromBody] TeacherDisciplineDTO dto)
        {
            try
            {
                var result = await _service.AddDiscipline(dto);
                return Ok(result);
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

        [HttpDelete("remove-discipline/{teacherId}/{disciplineId}")]
        public async Task<IActionResult> RemoveDiscipline(Guid teacherId, Guid disciplineId)
        {
            try
            {
                await _service.RemoveDiscipline(teacherId, disciplineId);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost("set-responsible/{teacherId}/{disciplineId}")]
        public async Task<IActionResult> SetAsResponsible(Guid teacherId, Guid disciplineId)
        {
            try
            {
                await _service.SetAsResponsible(teacherId, disciplineId);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
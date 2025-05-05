using EducaERP.Application.DTOs.Academics;
using EducaERP.Application.Interfaces.Academics;
using EducaERP.Core.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace EducaERP.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ApiExplorerSettings(GroupName = "Acadêmico")]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseService _service;

        public CoursesController(ICourseService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] bool? activeOnly = true)
        {
            var courses = await _service.GetAll(activeOnly);
            return Ok(courses);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var course = await _service.GetById(id);
                return Ok(course);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CourseDTO dto)
        {
            try
            {
                var course = await _service.Create(dto);
                return CreatedAtAction(nameof(GetById), new { id = course.Id }, course);
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] CourseDTO dto)
        {
            try
            {
                var course = await _service.Update(id, dto);
                return Ok(course);
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

        [HttpPost("add-discipline")]
        public async Task<IActionResult> AddDisciplineToCourse([FromBody] CourseDisciplineDTO dto)
        {
            try
            {
                await _service.AddDisciplineToCourse(dto);
                return NoContent();
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

        [HttpDelete("{courseId}/remove-discipline/{disciplineId}")]
        public async Task<IActionResult> RemoveDisciplineFromCourse(Guid courseId, Guid disciplineId)
        {
            try
            {
                await _service.RemoveDisciplineFromCourse(courseId, disciplineId);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
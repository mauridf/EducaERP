using EducaERP.Application.DTOs.Institutions;
using EducaERP.Application.Interfaces.Institutions;
using EducaERP.Core.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace EducaERP.API.Controllers
{
    [ApiController]
    [Route("api/instituicao")]
    [ApiExplorerSettings(GroupName = "Instituição")]
    public class InstitutionsController : ControllerBase
    {
        private readonly IInstitutionService _service;

        public InstitutionsController(IInstitutionService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var institutions = await _service.GetAll();
            return Ok(institutions);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var institution = await _service.GetById(id);
                return Ok(institution);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] InstitutionDTO dto)
        {
            try
            {
                var institution = await _service.Create(dto);
                return CreatedAtAction(nameof(GetById), new { id = institution.Id }, institution);
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] InstitutionDTO dto)
        {
            try
            {
                var institution = await _service.Update(id, dto);
                return Ok(institution);
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
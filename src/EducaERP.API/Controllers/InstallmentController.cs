using EducaERP.Application.DTOs.Financial;
using EducaERP.Application.Interfaces.Financial;
using EducaERP.Core.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace EducaERP.API.Controllers
{
    [ApiController]
    [Route("api/parcelamentos")]
    [ApiExplorerSettings(GroupName = "Financeiro")]
    public class InstallmentController : ControllerBase
    {
        private readonly IInstallmentService _service;

        public InstallmentController(IInstallmentService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] InstallmentDTO dto)
        {
            try
            {
                var result = await _service.Create(dto);
                return Created(string.Empty, result);
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

        [HttpGet("por-mensalidade/{mensalidadeId}")]
        public async Task<IActionResult> GetByTuition(Guid mensalidadeId)
        {
            try
            {
                var result = await _service.GetByTuition(mensalidadeId);
                return Ok(result);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPatch("{id}/pagar")]
        public async Task<IActionResult> ProcessPayment(Guid id)
        {
            try
            {
                await _service.ProcessInstallmentPayment(id);
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
    }
}
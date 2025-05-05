using EducaERP.Application.DTOs.Financial;
using EducaERP.Application.Interfaces.Financial;
using EducaERP.Core.Enums;
using EducaERP.Core.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace EducaERP.API.Controllers
{
    [ApiController]
    [Route("api/mensalidades")]
    [ApiExplorerSettings(GroupName = "Financeiro")]
    public class TuitionController : ControllerBase
    {
        private readonly ITuitionService _service;

        public TuitionController(ITuitionService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TuitionDTO dto)
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

        [HttpGet("por-referencia/{referencia}")]
        public async Task<IActionResult> GetByReference(string referencia)
        {
            var result = await _service.GetByReference(referencia);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] TuitionDTO dto)
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

        [HttpPatch("{id}/pagar")]
        public async Task<IActionResult> ProcessPayment(
            Guid id,
            [FromBody] PaymentRequest request)
        {
            try
            {
                await _service.ProcessPayment(id, request.ValorPago, request.FormaPagamento);
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

        public class PaymentRequest
        {
            public decimal ValorPago { get; set; }
            public PaymentMethod FormaPagamento { get; set; }
        }
    }
}
using EducaERP.Application.DTOs.Library;
using EducaERP.Application.Interfaces.Library;
using EducaERP.Core.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace EducaERP.API.Controllers
{
    [ApiController]
    [Route("api/emprestimos")]
    [ApiExplorerSettings(GroupName = "Biblioteca")]
    public class BookLoanController : ControllerBase
    {
        private readonly IBookLoanService _service;

        public BookLoanController(IBookLoanService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] BookLoanDTO dto)
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

        [HttpGet("ativos")]
        public async Task<IActionResult> GetActiveLoans()
        {
            var result = await _service.GetActiveLoans();
            return Ok(result);
        }

        [HttpGet("atrasados")]
        public async Task<IActionResult> GetOverdueLoans()
        {
            var result = await _service.GetOverdueLoans();
            return Ok(result);
        }

        [HttpGet("por-livro/{livroId}")]
        public async Task<IActionResult> GetByBook(Guid livroId)
        {
            var result = await _service.GetByBook(livroId);
            return Ok(result);
        }

        [HttpGet("por-usuario/{usuarioId}")]
        public async Task<IActionResult> GetByUser(Guid usuarioId)
        {
            var result = await _service.GetByUser(usuarioId);
            return Ok(result);
        }

        [HttpPatch("{id}/devolver")]
        public async Task<IActionResult> ReturnBook(Guid id)
        {
            try
            {
                await _service.ReturnBook(id);
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
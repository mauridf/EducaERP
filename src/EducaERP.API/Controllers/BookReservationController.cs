using EducaERP.Application.DTOs.Library;
using EducaERP.Application.Interfaces.Library;
using EducaERP.Core.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace EducaERP.API.Controllers
{
    [ApiController]
    [Route("api/reservas")]
    [ApiExplorerSettings(GroupName = "Biblioteca")]
    public class BookReservationController : ControllerBase
    {
        private readonly IBookReservationService _service;

        public BookReservationController(IBookReservationService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] BookReservationDTO dto)
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

        [HttpGet("ativas")]
        public async Task<IActionResult> GetActiveReservations()
        {
            var result = await _service.GetActiveReservations();
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

        [HttpPatch("{id}/cancelar")]
        public async Task<IActionResult> CancelReservation(Guid id)
        {
            try
            {
                await _service.CancelReservation(id);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPatch("{id}/concluir")]
        public async Task<IActionResult> CompleteReservation(Guid id)
        {
            try
            {
                await _service.CompleteReservation(id);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
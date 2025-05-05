using EducaERP.Application.DTOs.Library;
using EducaERP.Application.Interfaces.Library;
using EducaERP.Core.Enums;
using EducaERP.Core.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace EducaERP.API.Controllers
{
    [ApiController]
    [Route("api/livros")]
    [ApiExplorerSettings(GroupName = "Biblioteca")]
    public class BookController : ControllerBase
    {
        private readonly IBookService _service;

        public BookController(IBookService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] BookDTO dto)
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

        [HttpGet("buscar/{termo}")]
        public async Task<IActionResult> Search(string termo)
        {
            var result = await _service.Search(termo);
            return Ok(result);
        }

        [HttpGet("categoria/{categoria}")]
        public async Task<IActionResult> GetByCategory(BookCategory categoria)
        {
            var result = await _service.GetByCategory(categoria);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] BookDTO dto)
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

        [HttpPatch("{id}/estoque")]
        public async Task<IActionResult> UpdateStock(Guid id, [FromBody] int quantidade)
        {
            try
            {
                await _service.UpdateStock(id, quantidade);
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
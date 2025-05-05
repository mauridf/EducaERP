using EducaERP.Application.DTOs.Library;
using EducaERP.Application.DTOs.Library.Responses;
using EducaERP.Application.Interfaces.Library;
using EducaERP.Core.Domain.Library;
using EducaERP.Core.Enums;
using EducaERP.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducaERP.Application.Services.Library
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _repository;

        public BookService(IBookRepository repository)
        {
            _repository = repository;
        }

        public async Task<BookResponse> Create(BookDTO dto)
        {
            var existingBook = await _repository.GetByIsbnAsync(dto.ISBN);
            if (existingBook != null)
                throw new DomainException("Já existe um livro com este ISBN");

            var book = new Book(
                dto.Titulo,
                dto.Autor,
                dto.Editora,
                dto.AnoPublicacao,
                dto.ISBN,
                dto.Categoria,
                dto.QuantidadeTotal,
                dto.QuantidadeTotal);

            await _repository.AddAsync(book);

            return MapToResponse(book);
        }

        public async Task<BookResponse> Update(Guid id, BookDTO dto)
        {
            var book = await _repository.GetByIdAsync(id);
            if (book == null)
                throw new NotFoundException("Livro não encontrado");

            book.Update(
                dto.Titulo,
                dto.Autor,
                dto.Editora,
                dto.AnoPublicacao,
                dto.ISBN,
                dto.Categoria,
                dto.QuantidadeTotal,
                book.QuantidadeDisponivel);

            await _repository.UpdateAsync(book);

            return MapToResponse(book);
        }

        public async Task Delete(Guid id)
        {
            var book = await _repository.GetByIdAsync(id);
            if (book == null)
                throw new NotFoundException("Livro não encontrado");

            await _repository.DeleteAsync(book);
        }

        public async Task<BookResponse> GetById(Guid id)
        {
            var book = await _repository.GetByIdAsync(id);
            if (book == null)
                throw new NotFoundException("Livro não encontrado");

            return MapToResponse(book);
        }

        public async Task<IEnumerable<BookResponse>> GetAll()
        {
            var books = await _repository.GetAllAsync();
            return books.Select(MapToResponse);
        }

        public async Task<IEnumerable<BookResponse>> Search(string term)
        {
            var books = await _repository.SearchAsync(term);
            return books.Select(MapToResponse);
        }

        public async Task<IEnumerable<BookResponse>> GetByCategory(BookCategory category)
        {
            var books = await _repository.GetByCategoryAsync(category);
            return books.Select(MapToResponse);
        }

        public async Task UpdateStock(Guid id, int quantidade)
        {
            await _repository.UpdateStockAsync(id, quantidade);
        }

        private BookResponse MapToResponse(Book book)
        {
            return new BookResponse
            {
                Id = book.Id,
                Titulo = book.Titulo,
                Autor = book.Autor,
                Editora = book.Editora,
                AnoPublicacao = book.AnoPublicacao,
                ISBN = book.ISBN,
                Categoria = book.Categoria,
                QuantidadeTotal = book.QuantidadeTotal,
                QuantidadeDisponivel = book.QuantidadeDisponivel,
                EmprestimosAtivos = book.Emprestimos?.Count(e => e.Status == LoanStatus.Emprestado) ?? 0,
                ReservasAtivas = book.Reservas?.Count(r => r.Status == ReservationStatus.Ativa) ?? 0,
                DataCriacao = book.DataCriacao
            };
        }
    }
}
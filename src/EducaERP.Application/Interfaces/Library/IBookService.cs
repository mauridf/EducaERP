using EducaERP.Application.DTOs.Library;
using EducaERP.Application.DTOs.Library.Responses;
using EducaERP.Core.Enums;

namespace EducaERP.Application.Interfaces.Library
{
    public interface IBookService
    {
        Task<BookResponse> Create(BookDTO dto);
        Task<BookResponse> Update(Guid id, BookDTO dto);
        Task Delete(Guid id);
        Task<BookResponse> GetById(Guid id);
        Task<IEnumerable<BookResponse>> GetAll();
        Task<IEnumerable<BookResponse>> Search(string term);
        Task<IEnumerable<BookResponse>> GetByCategory(BookCategory category);
        Task UpdateStock(Guid id, int quantidade);
    }
}
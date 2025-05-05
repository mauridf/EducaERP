using EducaERP.Core.Domain.Library;
using EducaERP.Core.Enums;

namespace EducaERP.Application.Interfaces.Library
{
    public interface IBookRepository
    {
        Task<Book> GetByIdAsync(Guid id);
        Task<Book> GetByIsbnAsync(string isbn);
        Task<IEnumerable<Book>> GetAllAsync();
        Task<IEnumerable<Book>> SearchAsync(string term);
        Task<IEnumerable<Book>> GetByCategoryAsync(BookCategory category);
        Task AddAsync(Book book);
        Task UpdateAsync(Book book);
        Task DeleteAsync(Book book);
        Task UpdateStockAsync(Guid id, int quantidade);
    }
}
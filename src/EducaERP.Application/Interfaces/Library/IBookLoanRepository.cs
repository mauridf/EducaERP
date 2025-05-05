using EducaERP.Core.Domain.Library;

namespace EducaERP.Application.Interfaces.Library
{
    public interface IBookLoanRepository
    {
        Task<BookLoan> GetByIdAsync(Guid id);
        Task<IEnumerable<BookLoan>> GetAllAsync();
        Task<IEnumerable<BookLoan>> GetActiveLoansAsync();
        Task<IEnumerable<BookLoan>> GetOverdueLoansAsync();
        Task<IEnumerable<BookLoan>> GetByBookAsync(Guid livroId);
        Task<IEnumerable<BookLoan>> GetByUserAsync(Guid usuarioId);
        Task AddAsync(BookLoan loan);
        Task ReturnBookAsync(Guid id);
    }
}
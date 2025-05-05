using EducaERP.Application.DTOs.Library;
using EducaERP.Application.DTOs.Library.Responses;

namespace EducaERP.Application.Interfaces.Library
{
    public interface IBookLoanService
    {
        Task<BookLoanResponse> Create(BookLoanDTO dto);
        Task ReturnBook(Guid id);
        Task<BookLoanResponse> GetById(Guid id);
        Task<IEnumerable<BookLoanResponse>> GetAll();
        Task<IEnumerable<BookLoanResponse>> GetActiveLoans();
        Task<IEnumerable<BookLoanResponse>> GetOverdueLoans();
        Task<IEnumerable<BookLoanResponse>> GetByBook(Guid livroId);
        Task<IEnumerable<BookLoanResponse>> GetByUser(Guid usuarioId);
    }
}
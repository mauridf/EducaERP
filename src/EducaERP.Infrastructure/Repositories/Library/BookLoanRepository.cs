using EducaERP.Application.Interfaces.Library;
using EducaERP.Core.Domain.Library;
using EducaERP.Core.Enums;
using EducaERP.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EducaERP.Infrastructure.Repositories.Library
{
    public class BookLoanRepository : IBookLoanRepository
    {
        private readonly EducaERPDbContext _context;

        public BookLoanRepository(EducaERPDbContext context)
        {
            _context = context;
        }

        public async Task<BookLoan> GetByIdAsync(Guid id)
        {
            return await _context.Emprestimos
                .Include(e => e.Livro)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<IEnumerable<BookLoan>> GetAllAsync()
        {
            return await _context.Emprestimos
                .Include(e => e.Livro)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<BookLoan>> GetActiveLoansAsync()
        {
            return await _context.Emprestimos
                .Where(e => e.Status == LoanStatus.Emprestado)
                .Include(e => e.Livro)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<BookLoan>> GetOverdueLoansAsync()
        {
            var today = DateTime.UtcNow.Date;
            return await _context.Emprestimos
                .Where(e => e.Status == LoanStatus.Emprestado &&
                           e.DataPrevistaDevolucao < today)
                .Include(e => e.Livro)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<BookLoan>> GetByBookAsync(Guid livroId)
        {
            return await _context.Emprestimos
                .Where(e => e.LivroId == livroId)
                .Include(e => e.Livro)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<BookLoan>> GetByUserAsync(Guid usuarioId)
        {
            return await _context.Emprestimos
                .Where(e => e.AlunoId == usuarioId ||
                           e.ProfessorId == usuarioId ||
                           e.FuncionarioId == usuarioId)
                .Include(e => e.Livro)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task AddAsync(BookLoan loan)
        {
            await _context.Emprestimos.AddAsync(loan);
            await _context.SaveChangesAsync();
        }

        public async Task ReturnBookAsync(Guid id)
        {
            var loan = await _context.Emprestimos.FindAsync(id);
            if (loan != null)
            {
                loan.RegistrarDevolucao(DateTime.UtcNow);
                await _context.SaveChangesAsync();
            }
        }
    }
}
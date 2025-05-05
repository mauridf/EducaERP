using EducaERP.Application.Interfaces.Library;
using EducaERP.Core.Domain.Library;
using EducaERP.Core.Enums;
using EducaERP.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EducaERP.Infrastructure.Repositories.Library
{
    public class BookRepository : IBookRepository
    {
        private readonly EducaERPDbContext _context;

        public BookRepository(EducaERPDbContext context)
        {
            _context = context;
        }

        public async Task<Book> GetByIdAsync(Guid id)
        {
            return await _context.Livros
                .Include(b => b.Emprestimos)
                .Include(b => b.Reservas)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<Book> GetByIsbnAsync(string isbn)
        {
            return await _context.Livros
                .FirstOrDefaultAsync(b => b.ISBN == isbn);
        }

        public async Task<IEnumerable<Book>> GetAllAsync()
        {
            return await _context.Livros
                .Include(b => b.Emprestimos)
                .Include(b => b.Reservas)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<Book>> SearchAsync(string term)
        {
            return await _context.Livros
                .Where(b => b.Titulo.Contains(term) ||
                           b.Autor.Contains(term) ||
                           b.ISBN.Contains(term))
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<Book>> GetByCategoryAsync(BookCategory category)
        {
            return await _context.Livros
                .Where(b => b.Categoria == category)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task AddAsync(Book book)
        {
            await _context.Livros.AddAsync(book);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Book book)
        {
            _context.Livros.Update(book);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Book book)
        {
            _context.Livros.Remove(book);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateStockAsync(Guid id, int quantidade)
        {
            var book = await _context.Livros.FindAsync(id);
            if (book != null)
            {
                book.QuantidadeTotal = quantidade;
                book.QuantidadeDisponivel = quantidade -
                    await _context.Emprestimos
                        .CountAsync(e => e.LivroId == id && e.Status == LoanStatus.Emprestado);

                await _context.SaveChangesAsync();
            }
        }
    }
}
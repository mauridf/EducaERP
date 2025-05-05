using EducaERP.Core.Interfaces;
using EducaERP.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EducaERP.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly EducaERPDbContext _context;

        public Repository(EducaERPDbContext context)
        {
            _context = context;
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
        }

        public async Task DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
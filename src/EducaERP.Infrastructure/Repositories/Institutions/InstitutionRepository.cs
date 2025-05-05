using EducaERP.Application.Interfaces.Institutions;
using EducaERP.Core.Domain.Institutions;
using EducaERP.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EducaERP.Infrastructure.Repositories.Institutions
{
    public class InstitutionRepository : IInstitutionRepository
    {
        private readonly EducaERPDbContext _context;

        public InstitutionRepository(EducaERPDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Institution institution)
        {
            await _context.Instituicoes.AddAsync(institution);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> CnpjExistsAsync(string cnpj)
        {
            return await _context.Instituicoes
                .AnyAsync(i => i.Cnpj == cnpj);
        }

        public async Task DeleteAsync(Institution institution)
        {
            _context.Instituicoes.Remove(institution);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Institution>> GetAllAsync()
        {
            return await _context.Instituicoes.ToListAsync();
        }

        public async Task<Institution> GetByCnpjAsync(string cnpj)
        {
            return await _context.Instituicoes
                .FirstOrDefaultAsync(i => i.Cnpj == cnpj);
        }

        public async Task<Institution> GetByIdAsync(Guid id)
        {
            return await _context.Instituicoes.FindAsync(id);
        }

        public async Task UpdateAsync(Institution institution)
        {
            _context.Instituicoes.Update(institution);
            await _context.SaveChangesAsync();
        }
    }
}
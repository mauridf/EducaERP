using EducaERP.Application.Interfaces.Institutions;
using EducaERP.Core.Domain.Institutions;
using EducaERP.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducaERP.Infrastructure.Repositories.Institutions
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly EducaERPDbContext _context;

        public EmployeeRepository(EducaERPDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Employee employee)
        {
            await _context.Funcionarios.AddAsync(employee);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Employee employee)
        {
            _context.Funcionarios.Remove(employee);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            return await _context.Funcionarios
                .Include(e => e.Instituicao)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Employee> GetByIdAsync(Guid id)
        {
            return await _context.Funcionarios
                .Include(e => e.Instituicao)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task UpdateAsync(Employee employee)
        {
            _context.Funcionarios.Update(employee);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> CpfExistsAsync(string cpf)
        {
            return await _context.Funcionarios
                .AnyAsync(e => e.Cpf == cpf);
        }

        public async Task<Employee> GetByCpfAsync(string cpf)
        {
            return await _context.Funcionarios
                .FirstOrDefaultAsync(e => e.Cpf == cpf);
        }

        public async Task<IEnumerable<Employee>> GetByInstitutionAsync(Guid institutionId)
        {
            return await _context.Funcionarios
                .Where(e => e.InstituicaoId == institutionId)
                .Include(e => e.Instituicao)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
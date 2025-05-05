using EducaERP.Application.Interfaces.Students;
using EducaERP.Core.Domain.Students;
using EducaERP.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducaERP.Infrastructure.Repositories.Students
{
    public class StudentRepository : IStudentRepository
    {
        private readonly EducaERPDbContext _context;

        public StudentRepository(EducaERPDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Student student)
        {
            await _context.Alunos.AddAsync(student);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Student student)
        {
            _context.Alunos.Remove(student);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Student>> GetAllAsync()
        {
            return await _context.Alunos
                .Include(s => s.Instituicao)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Student> GetByIdAsync(Guid id)
        {
            return await _context.Alunos
                .Include(s => s.Instituicao)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task UpdateAsync(Student student)
        {
            _context.Alunos.Update(student);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> CpfExistsAsync(string cpf)
        {
            return await _context.Alunos
                .AnyAsync(s => s.Cpf == cpf);
        }

        public async Task<Student> GetByCpfAsync(string cpf)
        {
            return await _context.Alunos
                .FirstOrDefaultAsync(s => s.Cpf == cpf);
        }

        public async Task<IEnumerable<Student>> GetByInstitutionAsync(Guid institutionId)
        {
            return await _context.Alunos
                .Where(s => s.InstituicaoId == institutionId)
                .Include(s => s.Instituicao)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
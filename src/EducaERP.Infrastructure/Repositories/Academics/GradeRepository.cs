using EducaERP.Application.Interfaces.Academics;
using EducaERP.Core.Domain.Academics;
using EducaERP.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducaERP.Infrastructure.Repositories.Academics
{
    public class GradeRepository : IGradeRepository
    {
        private readonly EducaERPDbContext _context;

        public GradeRepository(EducaERPDbContext context)
        {
            _context = context;
        }

        public async Task<Grade> GetByIdAsync(Guid id)
        {
            return await _context.Notas
                .Include(g => g.Aluno)
                .Include(g => g.Disciplina)
                .FirstOrDefaultAsync(g => g.Id == id);
        }

        public async Task<IEnumerable<Grade>> GetByStudentAsync(Guid studentId)
        {
            return await _context.Notas
                .Where(g => g.AlunoId == studentId)
                .Include(g => g.Disciplina)
                .ToListAsync();
        }

        public async Task<IEnumerable<Grade>> GetByDisciplineAsync(Guid disciplineId)
        {
            return await _context.Notas
                .Where(g => g.DisciplinaId == disciplineId)
                .Include(g => g.Aluno)
                .ToListAsync();
        }

        public async Task AddAsync(Grade grade)
        {
            await _context.Notas.AddAsync(grade);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Grade grade)
        {
            _context.Notas.Update(grade);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Grade grade)
        {
            _context.Notas.Remove(grade);
            await _context.SaveChangesAsync();
        }

        public async Task<decimal> CalculateAverageAsync(Guid studentId, Guid disciplineId)
        {
            var grades = await _context.Notas
                .Where(g => g.AlunoId == studentId && g.DisciplinaId == disciplineId)
                .ToListAsync();

            if (!grades.Any())
                return 0;

            decimal totalWeighted = grades.Sum(g => g.NotaObtida * g.Peso);
            decimal totalWeight = grades.Sum(g => g.Peso);

            return totalWeight > 0 ? totalWeighted / totalWeight : 0;
        }

        public async Task<IEnumerable<Grade>> GetAllAsync()
        {
            return await _context.Notas
                .Include(g => g.Aluno)    // Carrega os dados do aluno
                .Include(g => g.Disciplina) // Carrega os dados da disciplina
                .AsNoTracking()             // Melhora performance para operações de leitura
                .ToListAsync();
        }
    }
}
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
    public class DisciplineRepository : IDisciplineRepository
    {
        private readonly EducaERPDbContext _context;

        public DisciplineRepository(EducaERPDbContext context)
        {
            _context = context;
        }

        public async Task<Discipline> GetByIdAsync(Guid id)
        {
            return await _context.Disciplinas
                .Include(d => d.Cursos)
                    .ThenInclude(cd => cd.Curso)
                .Include(d => d.Professores)
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<Discipline> GetByCodeAsync(string codigo)
        {
            return await _context.Disciplinas
                .FirstOrDefaultAsync(d => d.Codigo == codigo);
        }

        public async Task<IEnumerable<Discipline>> GetAllAsync(bool? activeOnly = true)
        {
            var query = _context.Disciplinas
                .Include(d => d.Cursos)
                .Include(d => d.Professores)
                .AsQueryable();

            if (activeOnly.HasValue)
                query = query.Where(d => d.Ativo == activeOnly.Value);

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<Discipline>> GetByCourseAsync(Guid courseId)
        {
            return await _context.Disciplinas
                .Where(d => d.Cursos.Any(cd => cd.CursoId == courseId))
                .ToListAsync();
        }

        public async Task AddAsync(Discipline discipline)
        {
            await _context.Disciplinas.AddAsync(discipline);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Discipline discipline)
        {
            _context.Disciplinas.Update(discipline);
            await _context.SaveChangesAsync();
        }

        public async Task ToggleStatusAsync(Guid id)
        {
            var discipline = await _context.Disciplinas.FindAsync(id);
            if (discipline != null)
            {
                discipline.Ativo = !discipline.Ativo;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsByCodeAsync(string code)
        {
            return await _context.Disciplinas
                .AnyAsync(d => d.Codigo == code);
        }
    }
}
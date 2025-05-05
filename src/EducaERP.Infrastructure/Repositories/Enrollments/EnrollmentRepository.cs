using EducaERP.Application.Interfaces.Enrollments;
using EducaERP.Core.Domain.Enrollments;
using EducaERP.Core.Enums;
using EducaERP.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EducaERP.Infrastructure.Repositories.Enrollments
{
    public class EnrollmentRepository : IEnrollmentRepository
    {
        private readonly EducaERPDbContext _context;

        public EnrollmentRepository(EducaERPDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Enrollment enrollment)
        {
            await _context.Matriculas.AddAsync(enrollment);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Enrollment enrollment)
        {
            _context.Matriculas.Remove(enrollment);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Enrollment>> GetAllAsync()
        {
            return await _context.Matriculas
                .Include(m => m.Aluno)
                .Include(m => m.Curso)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<Enrollment>> GetByCourseAsync(Guid cursoId)
        {
            return await _context.Matriculas
                .Where(m => m.CursoId == cursoId)
                .Include(m => m.Aluno)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Enrollment> GetByIdAsync(Guid id)
        {
            return await _context.Matriculas
                .Include(m => m.Aluno)
                .Include(m => m.Curso)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<IEnumerable<Enrollment>> GetByStudentAsync(Guid alunoId)
        {
            return await _context.Matriculas
                .Where(m => m.AlunoId == alunoId)
                .Include(m => m.Curso)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<Enrollment>> GetByYearAsync(string anoLetivo)
        {
            return await _context.Matriculas
                .Where(m => m.AnoLetivo == anoLetivo)
                .Include(m => m.Aluno)
                .Include(m => m.Curso)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task UpdateAsync(Enrollment enrollment)
        {
            _context.Matriculas.Update(enrollment);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateStatusAsync(Guid id, EnrollmentStatus newStatus)
        {
            var matricula = await _context.Matriculas.FindAsync(id);
            if (matricula != null)
            {
                matricula.Status = newStatus;
                await _context.SaveChangesAsync();
            }
        }
    }
}
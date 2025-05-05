using EducaERP.Application.Interfaces.Academics;
using EducaERP.Core.Domain.Academics;
using EducaERP.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EducaERP.Infrastructure.Repositories.Academics
{
    public class AttendanceRepository : IAttendanceRepository
    {
        private readonly EducaERPDbContext _context;

        public AttendanceRepository(EducaERPDbContext context)
        {
            _context = context;
        }

        public async Task<Attendance> GetByIdAsync(Guid id)
        {
            return await _context.Frequencias
                .Include(a => a.Aluno)
                .Include(a => a.Disciplina)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<IEnumerable<Attendance>> GetByStudentAsync(Guid studentId)
        {
            return await _context.Frequencias
                .Where(a => a.AlunoId == studentId)
                .Include(a => a.Disciplina)
                .ToListAsync();
        }

        public async Task<IEnumerable<Attendance>> GetByDisciplineAsync(Guid disciplineId)
        {
            return await _context.Frequencias
                .Where(a => a.DisciplinaId == disciplineId)
                .Include(a => a.Aluno)
                .ToListAsync();
        }

        public async Task<IEnumerable<Attendance>> GetByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await _context.Frequencias
                .Where(a => a.DataAula >= startDate && a.DataAula <= endDate)
                .Include(a => a.Aluno)
                .Include(a => a.Disciplina)
                .ToListAsync();
        }

        public async Task AddAsync(Attendance attendance)
        {
            await _context.Frequencias.AddAsync(attendance);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Attendance attendance)
        {
            _context.Frequencias.Update(attendance);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Attendance attendance)
        {
            _context.Frequencias.Remove(attendance);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Attendance>> GetAllAsync()
        {
            return await _context.Frequencias
                .Include(a => a.Aluno)  // Carrega os dados do aluno
                .Include(a => a.Disciplina) // Carrega os dados da disciplina
                .AsNoTracking() // Para melhor performance em operações somente leitura
                .ToListAsync();
        }
    }
}
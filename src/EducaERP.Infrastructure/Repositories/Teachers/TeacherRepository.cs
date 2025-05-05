using EducaERP.Application.Interfaces.Teachers;
using EducaERP.Core.Domain.Teachers;
using EducaERP.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EducaERP.Infrastructure.Repositories.Teachers
{
    public class TeacherRepository : ITeacherRepository
    {
        private readonly EducaERPDbContext _context;

        public TeacherRepository(EducaERPDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Teacher teacher)
        {
            await _context.Professores.AddAsync(teacher);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Teacher teacher)
        {
            _context.Professores.Remove(teacher);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Teacher>> GetAllAsync()
        {
            return await _context.Professores
                .Include(t => t.Instituicao)
                .Include(t => t.Disciplinas)
                    .ThenInclude(td => td.Disciplina)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Teacher> GetByIdAsync(Guid id)
        {
            return await _context.Professores
                .Include(t => t.Instituicao)
                .Include(t => t.Disciplinas)
                    .ThenInclude(td => td.Disciplina)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task UpdateAsync(Teacher teacher)
        {
            _context.Professores.Update(teacher);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> CpfExistsAsync(string cpf)
        {
            return await _context.Professores
                .AnyAsync(t => t.Cpf == cpf);
        }

        public async Task<Teacher> GetByCpfAsync(string cpf)
        {
            return await _context.Professores
                .FirstOrDefaultAsync(t => t.Cpf == cpf);
        }

        public async Task<IEnumerable<Teacher>> GetByInstitutionAsync(Guid institutionId)
        {
            return await _context.Professores
                .Where(t => t.InstituicaoId == institutionId)
                .Include(t => t.Instituicao)
                .Include(t => t.Disciplinas)
                    .ThenInclude(td => td.Disciplina)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<TeacherDiscipline> GetTeacherDisciplineAsync(Guid teacherId, Guid disciplineId)
        {
            return await _context.ProfessorDisciplinas
                .Include(td => td.Disciplina)
                .FirstOrDefaultAsync(td => td.ProfessorId == teacherId && td.DisciplinaId == disciplineId);
        }

        public async Task AddTeacherDisciplineAsync(TeacherDiscipline teacherDiscipline)
        {
            await _context.ProfessorDisciplinas.AddAsync(teacherDiscipline);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTeacherDisciplineAsync(TeacherDiscipline teacherDiscipline)
        {
            _context.ProfessorDisciplinas.Update(teacherDiscipline);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveTeacherDisciplineAsync(TeacherDiscipline teacherDiscipline)
        {
            _context.ProfessorDisciplinas.Remove(teacherDiscipline);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TeacherDiscipline>> GetTeacherDisciplinesAsync(Guid teacherId)
        {
            return await _context.ProfessorDisciplinas
                .Where(td => td.ProfessorId == teacherId)
                .Include(td => td.Disciplina)
                .ToListAsync();
        }
    }
}
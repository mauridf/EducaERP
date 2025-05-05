using EducaERP.Application.Interfaces.Academics;
using EducaERP.Core.Domain.Academics;
using EducaERP.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EducaERP.Infrastructure.Repositories.Academics
{
    public class CourseRepository : ICourseRepository, ICourseDisciplineRepository
    {
        private readonly EducaERPDbContext _context;

        public CourseRepository(EducaERPDbContext context)
        {
            _context = context;
        }

        public async Task<Course> GetByIdAsync(Guid id)
        {
            return await _context.Cursos
                .Include(c => c.Disciplinas)
                    .ThenInclude(cd => cd.Disciplina)
                .Include(c => c.Matriculas)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Course> GetByCodeAsync(string codigo)
        {
            return await _context.Cursos
                .FirstOrDefaultAsync(c => c.Codigo == codigo);
        }

        public async Task<IEnumerable<Course>> GetAllAsync(bool? activeOnly = true)
        {
            var query = _context.Cursos
                .Include(c => c.Disciplinas)
                .Include(c => c.Matriculas)
                .AsQueryable();

            if (activeOnly.HasValue)
                query = query.Where(c => c.Ativo == activeOnly.Value);

            return await query.ToListAsync();
        }

        public async Task AddAsync(Course course)
        {
            await _context.Cursos.AddAsync(course);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Course course)
        {
            _context.Cursos.Update(course);
            await _context.SaveChangesAsync();
        }

        public async Task ToggleStatusAsync(Guid id)
        {
            var course = await _context.Cursos.FindAsync(id);
            if (course != null)
            {
                course.Ativo = !course.Ativo;
                await _context.SaveChangesAsync();
            }
        }

        public async Task AddDisciplineToCourseAsync(CourseDiscipline courseDiscipline)
        {
            await _context.CursoDisciplinas.AddAsync(courseDiscipline);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveDisciplineFromCourseAsync(CourseDiscipline courseDiscipline)
        {
            _context.CursoDisciplinas.Remove(courseDiscipline);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCourseDisciplineAsync(CourseDiscipline courseDiscipline)
        {
            _context.CursoDisciplinas.Update(courseDiscipline);
            await _context.SaveChangesAsync();
        }

        public async Task<CourseDiscipline> GetCourseDisciplineAsync(Guid courseId, Guid disciplineId)
        {
            return await _context.CursoDisciplinas
                .Include(cd => cd.Disciplina)
                .FirstOrDefaultAsync(cd => cd.CursoId == courseId && cd.DisciplinaId == disciplineId);
        }

        public async Task<IEnumerable<CourseDiscipline>> GetCourseDisciplinesAsync(Guid courseId)
        {
            return await _context.CursoDisciplinas
                .Where(cd => cd.CursoId == courseId)
                .Include(cd => cd.Disciplina)
                .ToListAsync();
        }

        public async Task<IEnumerable<CourseDiscipline>> GetDisciplinesByCourseAsync(Guid courseId)
        {
            return await _context.CursoDisciplinas
                .Where(cd => cd.CursoId == courseId)
                .Include(cd => cd.Disciplina)  // Carrega os dados da disciplina
                .Include(cd => cd.Curso)      // Carrega os dados do curso (opcional)
                .OrderBy(cd => cd.Ordem)      // Ordena pela ordem definida
                .AsNoTracking()               // Melhora performance para operações de leitura
                .ToListAsync();
        }
    }
}
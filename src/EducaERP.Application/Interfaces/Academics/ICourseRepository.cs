using EducaERP.Core.Domain.Academics;

namespace EducaERP.Application.Interfaces.Academics
{
    public interface ICourseRepository
    {
        // Métodos básicos do curso
        Task<Course> GetByIdAsync(Guid id);
        Task<Course> GetByCodeAsync(string code);
        Task<IEnumerable<Course>> GetAllAsync(bool? activeOnly = true);
        Task AddAsync(Course course);
        Task UpdateAsync(Course course);
        Task ToggleStatusAsync(Guid id);

        // Métodos para gerenciar disciplinas do curso
        Task<CourseDiscipline> GetCourseDisciplineAsync(Guid courseId, Guid disciplineId);
        Task AddDisciplineToCourseAsync(CourseDiscipline courseDiscipline);
        Task RemoveDisciplineFromCourseAsync(CourseDiscipline courseDiscipline);

        // Outros métodos que podem ser necessários
        Task<IEnumerable<CourseDiscipline>> GetDisciplinesByCourseAsync(Guid courseId);
        Task UpdateCourseDisciplineAsync(CourseDiscipline courseDiscipline);
    }
}
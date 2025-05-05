using EducaERP.Core.Domain.Academics;

namespace EducaERP.Application.Interfaces.Academics
{
    public interface ICourseDisciplineRepository
    {
        Task AddDisciplineToCourseAsync(CourseDiscipline courseDiscipline);
        Task RemoveDisciplineFromCourseAsync(CourseDiscipline courseDiscipline);
        Task UpdateCourseDisciplineAsync(CourseDiscipline courseDiscipline);
        Task<CourseDiscipline> GetCourseDisciplineAsync(Guid courseId, Guid disciplineId);
        Task<IEnumerable<CourseDiscipline>> GetCourseDisciplinesAsync(Guid courseId);
    }
}

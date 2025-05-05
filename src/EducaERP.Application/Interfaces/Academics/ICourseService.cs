using EducaERP.Application.DTOs.Academics;
using EducaERP.Application.DTOs.Academics.Responses;

namespace EducaERP.Application.Interfaces.Academics
{
    public interface ICourseService
    {
        Task<CourseResponse> Create(CourseDTO dto);
        Task<CourseResponse> Update(Guid id, CourseDTO dto);
        Task ToggleStatus(Guid id);
        Task<CourseResponse> GetById(Guid id);
        Task<IEnumerable<CourseResponse>> GetAll(bool? activeOnly = true);
        Task AddDisciplineToCourse(CourseDisciplineDTO dto);
        Task RemoveDisciplineFromCourse(Guid courseId, Guid disciplineId);
    }
}
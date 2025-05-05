using EducaERP.Application.DTOs.Teachers;
using EducaERP.Application.DTOs.Teachers.Responses;

namespace EducaERP.Application.Interfaces.Teachers
{
    public interface ITeacherService
    {
        Task<TeacherResponse> Create(TeacherDTO dto);
        Task<TeacherResponse> Update(Guid id, TeacherDTO dto);
        Task Delete(Guid id);
        Task<TeacherResponse> GetById(Guid id);
        Task<IEnumerable<TeacherResponse>> GetAll();
        Task<IEnumerable<TeacherResponse>> GetByInstitution(Guid institutionId);
        Task<TeacherDisciplineResponse> AddDiscipline(TeacherDisciplineDTO dto);
        Task RemoveDiscipline(Guid teacherId, Guid disciplineId);
        Task SetAsResponsible(Guid teacherId, Guid disciplineId);
    }
}
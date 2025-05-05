using EducaERP.Application.DTOs.Students;
using EducaERP.Application.DTOs.Students.Responses;

namespace EducaERP.Application.Interfaces.Students
{
    public interface IStudentService
    {
        Task<StudentResponse> Create(StudentDTO dto);
        Task<StudentResponse> Update(Guid id, StudentDTO dto);
        Task Delete(Guid id);
        Task<StudentResponse> GetById(Guid id);
        Task<IEnumerable<StudentResponse>> GetAll();
        Task<IEnumerable<StudentResponse>> GetByInstitution(Guid institutionId);
    }
}
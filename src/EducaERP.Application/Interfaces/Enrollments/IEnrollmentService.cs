using EducaERP.Application.DTOs.Enrollments;
using EducaERP.Application.DTOs.Enrollments.Responses;
using EducaERP.Core.Enums;

namespace EducaERP.Application.Interfaces.Enrollments
{
    public interface IEnrollmentService
    {
        Task<EnrollmentResponse> Create(EnrollmentDTO dto);
        Task<EnrollmentResponse> Update(Guid id, EnrollmentDTO dto);
        Task Delete(Guid id);
        Task<EnrollmentResponse> GetById(Guid id);
        Task<IEnumerable<EnrollmentResponse>> GetAll();
        Task<IEnumerable<EnrollmentResponse>> GetByStudent(Guid alunoId);
        Task<IEnumerable<EnrollmentResponse>> GetByCourse(Guid cursoId);
        Task<IEnumerable<EnrollmentResponse>> GetByYear(string anoLetivo);
        Task ChangeStatus(Guid id, EnrollmentStatus newStatus);
    }
}
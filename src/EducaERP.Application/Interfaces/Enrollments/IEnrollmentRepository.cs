using EducaERP.Core.Domain.Enrollments;
using EducaERP.Core.Enums;

namespace EducaERP.Application.Interfaces.Enrollments
{
    public interface IEnrollmentRepository
    {
        Task<Enrollment> GetByIdAsync(Guid id);
        Task<IEnumerable<Enrollment>> GetAllAsync();
        Task<IEnumerable<Enrollment>> GetByStudentAsync(Guid alunoId);
        Task<IEnumerable<Enrollment>> GetByCourseAsync(Guid cursoId);
        Task<IEnumerable<Enrollment>> GetByYearAsync(string anoLetivo);
        Task AddAsync(Enrollment enrollment);
        Task UpdateAsync(Enrollment enrollment);
        Task DeleteAsync(Enrollment enrollment);
        Task UpdateStatusAsync(Guid id, EnrollmentStatus newStatus);
    }
}
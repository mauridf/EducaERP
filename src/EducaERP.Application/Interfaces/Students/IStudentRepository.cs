using EducaERP.Core.Domain.Students;

namespace EducaERP.Application.Interfaces.Students
{
    public interface IStudentRepository
    {
        Task<Student> GetByIdAsync(Guid id);
        Task<IEnumerable<Student>> GetAllAsync();
        Task<IEnumerable<Student>> GetByInstitutionAsync(Guid institutionId);
        Task AddAsync(Student student);
        Task UpdateAsync(Student student);
        Task DeleteAsync(Student student);
        Task<bool> CpfExistsAsync(string cpf);
        Task<Student> GetByCpfAsync(string cpf);
    }
}
using EducaERP.Core.Domain.Teachers;

namespace EducaERP.Application.Interfaces.Teachers
{
    public interface ITeacherRepository
    {
        Task<Teacher> GetByIdAsync(Guid id);
        Task<IEnumerable<Teacher>> GetAllAsync();
        Task<IEnumerable<Teacher>> GetByInstitutionAsync(Guid institutionId);
        Task AddAsync(Teacher teacher);
        Task UpdateAsync(Teacher teacher);
        Task DeleteAsync(Teacher teacher);
        Task<bool> CpfExistsAsync(string cpf);
        Task<Teacher> GetByCpfAsync(string cpf);

        // TeacherDiscipline methods
        Task<TeacherDiscipline> GetTeacherDisciplineAsync(Guid teacherId, Guid disciplineId);
        Task AddTeacherDisciplineAsync(TeacherDiscipline teacherDiscipline);
        Task UpdateTeacherDisciplineAsync(TeacherDiscipline teacherDiscipline);
        Task RemoveTeacherDisciplineAsync(TeacherDiscipline teacherDiscipline);
        Task<IEnumerable<TeacherDiscipline>> GetTeacherDisciplinesAsync(Guid teacherId);
    }
}
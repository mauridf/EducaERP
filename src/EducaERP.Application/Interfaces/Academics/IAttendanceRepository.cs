using EducaERP.Core.Domain.Academics;

namespace EducaERP.Application.Interfaces.Academics
{
    public interface IAttendanceRepository
    {
        Task<Attendance> GetByIdAsync(Guid id);
        Task<IEnumerable<Attendance>> GetAllAsync();
        Task<IEnumerable<Attendance>> GetByStudentAsync(Guid studentId);
        Task<IEnumerable<Attendance>> GetByDisciplineAsync(Guid disciplineId);
        Task<IEnumerable<Attendance>> GetByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task AddAsync(Attendance attendance);
        Task UpdateAsync(Attendance attendance);
        Task DeleteAsync(Attendance attendance);
    }
}
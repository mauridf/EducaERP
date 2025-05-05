using EducaERP.Application.DTOs.Academics;
using EducaERP.Application.DTOs.Academics.Responses;

namespace EducaERP.Application.Interfaces.Academics
{
    public interface IAttendanceService
    {
        Task<IEnumerable<AttendanceResponse>> GetAll();
        Task<AttendanceResponse> Register(AttendanceDTO dto);
        Task<AttendanceResponse> Update(Guid id, AttendanceDTO dto);
        Task Delete(Guid id);
        Task<AttendanceResponse> GetById(Guid id);
        Task<IEnumerable<AttendanceResponse>> GetByStudent(Guid studentId);
        Task<IEnumerable<AttendanceResponse>> GetByDiscipline(Guid disciplineId);
        Task<IEnumerable<AttendanceResponse>> GetByDateRange(DateTime startDate, DateTime endDate);
    }
}
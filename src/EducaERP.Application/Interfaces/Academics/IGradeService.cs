using EducaERP.Application.DTOs.Academics;
using EducaERP.Application.DTOs.Academics.Responses;

namespace EducaERP.Application.Interfaces.Academics
{
    public interface IGradeService
    {
        Task<IEnumerable<GradeResponse>> GetAll();
        Task<GradeResponse> Register(GradeDTO dto);
        Task<GradeResponse> Update(Guid id, GradeDTO dto);
        Task Delete(Guid id);
        Task<GradeResponse> GetById(Guid id);
        Task<IEnumerable<GradeResponse>> GetByStudent(Guid studentId);
        Task<IEnumerable<GradeResponse>> GetByDiscipline(Guid disciplineId);
        Task<decimal> CalculateAverage(Guid studentId, Guid disciplineId);
    }
}
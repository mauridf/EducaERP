using EducaERP.Application.DTOs.Academics;
using EducaERP.Application.DTOs.Academics.Responses;

namespace EducaERP.Application.Interfaces.Academics
{
    public interface IDisciplineService
    {
        Task<DisciplineResponse> Create(DisciplineDTO dto);
        Task<DisciplineResponse> Update(Guid id, DisciplineDTO dto);
        Task ToggleStatus(Guid id);
        Task<DisciplineResponse> GetById(Guid id);
        Task<IEnumerable<DisciplineResponse>> GetAll(bool? activeOnly = true);
        Task<IEnumerable<DisciplineResponse>> GetByCourse(Guid courseId);
    }
}
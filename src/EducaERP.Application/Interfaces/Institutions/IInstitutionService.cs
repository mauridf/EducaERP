using EducaERP.Application.DTOs.Institutions;
using EducaERP.Application.DTOs.Institutions.Responses;

namespace EducaERP.Application.Interfaces.Institutions
{
    public interface IInstitutionService
    {
        Task<InstitutionResponse> Create(InstitutionDTO dto);
        Task<InstitutionResponse> Update(Guid id, InstitutionDTO dto);
        Task Delete(Guid id);
        Task<InstitutionResponse> GetById(Guid id);
        Task<IEnumerable<InstitutionResponse>> GetAll();
    }
}
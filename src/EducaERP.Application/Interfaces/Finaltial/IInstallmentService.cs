using EducaERP.Application.DTOs.Financial;
using EducaERP.Application.DTOs.Financial.Responses;

namespace EducaERP.Application.Interfaces.Financial
{
    public interface IInstallmentService
    {
        Task<InstallmentResponse> Create(InstallmentDTO dto);
        Task ProcessInstallmentPayment(Guid id);
        Task<IEnumerable<InstallmentResponse>> GetByTuition(Guid mensalidadeId);
        Task<InstallmentResponse> GetById(Guid id);
    }
}
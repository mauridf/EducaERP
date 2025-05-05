using EducaERP.Core.Domain.Financial;

namespace EducaERP.Application.Interfaces.Financial
{
    public interface IInstallmentRepository
    {
        Task<Installment> GetByIdAsync(Guid id);
        Task<IEnumerable<Installment>> GetByTuitionAsync(Guid mensalidadeId);
        Task AddAsync(Installment installment);
        Task ProcessPaymentAsync(Guid id);
    }
}
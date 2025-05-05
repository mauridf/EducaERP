using EducaERP.Core.Domain.Financial;
using EducaERP.Core.Enums;

namespace EducaERP.Application.Interfaces.Financial
{
    public interface ITuitionRepository
    {
        Task<Tuition> GetByIdAsync(Guid id);
        Task<IEnumerable<Tuition>> GetAllAsync();
        Task<IEnumerable<Tuition>> GetByStudentAsync(Guid alunoId);
        Task<IEnumerable<Tuition>> GetByCourseAsync(Guid cursoId);
        Task<IEnumerable<Tuition>> GetByReferenceAsync(string referencia);
        Task AddAsync(Tuition tuition);
        Task UpdateAsync(Tuition tuition);
        Task DeleteAsync(Tuition tuition);
        Task ProcessPaymentAsync(Guid id, decimal valorPago, PaymentMethod formaPagamento);
    }
}
using EducaERP.Application.DTOs.Financial;
using EducaERP.Application.DTOs.Financial.Responses;
using EducaERP.Core.Enums;

namespace EducaERP.Application.Interfaces.Financial
{
    public interface ITuitionService
    {
        Task<TuitionResponse> Create(TuitionDTO dto);
        Task<TuitionResponse> Update(Guid id, TuitionDTO dto);
        Task Delete(Guid id);
        Task<TuitionResponse> GetById(Guid id);
        Task<IEnumerable<TuitionResponse>> GetAll();
        Task<IEnumerable<TuitionResponse>> GetByStudent(Guid alunoId);
        Task<IEnumerable<TuitionResponse>> GetByCourse(Guid cursoId);
        Task<IEnumerable<TuitionResponse>> GetByReference(string referencia);
        Task ProcessPayment(Guid id, decimal valorPago, PaymentMethod formaPagamento);
    }
}
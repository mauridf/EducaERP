using EducaERP.Application.DTOs.Financial;
using EducaERP.Application.DTOs.Financial.Responses;
using EducaERP.Application.Interfaces.Financial;
using EducaERP.Core.Domain.Financial;
using EducaERP.Core.Exceptions;

namespace EducaERP.Application.Services.Financial
{
    public class InstallmentService : IInstallmentService
    {
        private readonly IInstallmentRepository _repository;
        private readonly ITuitionRepository _tuitionRepository;

        public InstallmentService(
            IInstallmentRepository repository,
            ITuitionRepository tuitionRepository)
        {
            _repository = repository;
            _tuitionRepository = tuitionRepository;
        }

        public async Task<InstallmentResponse> Create(InstallmentDTO dto)
        {
            var mensalidade = await _tuitionRepository.GetByIdAsync(dto.MensalidadeId);
            if (mensalidade == null)
                throw new NotFoundException("Mensalidade não encontrada");

            var parcela = new Installment(
                dto.MensalidadeId,
                await GetNextInstallmentNumber(dto.MensalidadeId),
                dto.ValorParcela,
                dto.DataVencimento,
                false);

            await _repository.AddAsync(parcela);

            return MapToResponse(parcela);
        }

        public async Task<IEnumerable<InstallmentResponse>> GetByTuition(Guid mensalidadeId)
        {
            var parcelas = await _repository.GetByTuitionAsync(mensalidadeId);
            return parcelas.Select(MapToResponse);
        }

        public async Task ProcessInstallmentPayment(Guid id)
        {
            await _repository.ProcessPaymentAsync(id);
        }

        public async Task<InstallmentResponse> GetById(Guid id)
        {
            var parcela = await _repository.GetByIdAsync(id);
            if (parcela == null)
                throw new NotFoundException("Parcela não encontrada");

            return MapToResponse(parcela);
        }

        private InstallmentResponse MapToResponse(Installment installment)
        {
            return new InstallmentResponse
            {
                Id = installment.Id,
                MensalidadeId = installment.MensalidadeId,
                ParcelaNumero = installment.ParcelaNumero,
                ValorParcela = installment.ValorParcela,
                DataVencimento = installment.DataVencimento,
                Pago = installment.Pago,
                DataPagamento = installment.Pago ? installment.DataPagamento : null
            };
        }

        private async Task<int> GetNextInstallmentNumber(Guid mensalidadeId)
        {
            var parcelas = await _repository.GetByTuitionAsync(mensalidadeId);
            return parcelas.Count() + 1;
        }
    }
}
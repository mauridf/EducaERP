using EducaERP.Application.DTOs.Financial;
using EducaERP.Application.DTOs.Financial.Responses;
using EducaERP.Application.Interfaces.Academics;
using EducaERP.Application.Interfaces.Financial;
using EducaERP.Application.Interfaces.Students;
using EducaERP.Core.Domain.Financial;
using EducaERP.Core.Enums;
using EducaERP.Core.Exceptions;

namespace EducaERP.Application.Services.Financial
{
    public class TuitionService : ITuitionService
    {
        private readonly ITuitionRepository _repository;
        private readonly IStudentRepository _studentRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly IInstallmentService _installmentService;

        public TuitionService(
            ITuitionRepository repository,
            IStudentRepository studentRepository,
            ICourseRepository courseRepository,
            IInstallmentService installmentService)
        {
            _repository = repository;
            _studentRepository = studentRepository;
            _courseRepository = courseRepository;
            _installmentService = installmentService;
        }

        public async Task<TuitionResponse> Create(TuitionDTO dto)
        {
            var aluno = await _studentRepository.GetByIdAsync(dto.AlunoId);
            if (aluno == null)
                throw new NotFoundException("Aluno não encontrado");

            var curso = await _courseRepository.GetByIdAsync(dto.CursoId);
            if (curso == null)
                throw new NotFoundException("Curso não encontrado");

            var mensalidade = new Tuition(
                dto.AlunoId,
                dto.CursoId,
                dto.Referencia,
                dto.Valor,
                dto.DataVencimento,
                PaymentStatus.Pendente,
                dto.DataVencimento,
                dto.FormaPagamento);

            await _repository.AddAsync(mensalidade);

            // Se for parcelado
            if (dto.NumeroParcelas > 1)
            {
                await CreateInstallments(mensalidade, dto);
            }

            return MapToResponse(mensalidade, aluno.Nome, curso.Nome);
        }

        public async Task Delete(Guid id)
        {
            var mensalidade = await _repository.GetByIdAsync(id);
            if (mensalidade == null)
                throw new NotFoundException("Mensalidade não encontrada");

            await _repository.DeleteAsync(mensalidade);
        }

        public async Task<IEnumerable<TuitionResponse>> GetAll()
        {
            var mensalidades = await _repository.GetAllAsync();
            var responses = new List<TuitionResponse>();

            foreach (var mensalidade in mensalidades)
            {
                var aluno = await _studentRepository.GetByIdAsync(mensalidade.AlunoId);
                var curso = await _courseRepository.GetByIdAsync(mensalidade.CursoId);
                responses.Add(MapToResponse(mensalidade, aluno?.Nome, curso?.Nome));
            }

            return responses;
        }

        public async Task<IEnumerable<TuitionResponse>> GetByCourse(Guid cursoId)
        {
            var curso = await _courseRepository.GetByIdAsync(cursoId);
            if (curso == null)
                throw new NotFoundException("Curso não encontrado");

            var mensalidades = await _repository.GetByCourseAsync(cursoId);
            return mensalidades.Select(m => MapToResponse(m, m.Aluno?.Nome, curso.Nome));
        }

        public async Task<TuitionResponse> GetById(Guid id)
        {
            var mensalidade = await _repository.GetByIdAsync(id);
            if (mensalidade == null)
                throw new NotFoundException("Mensalidade não encontrada");

            var aluno = await _studentRepository.GetByIdAsync(mensalidade.AlunoId);
            var curso = await _courseRepository.GetByIdAsync(mensalidade.CursoId);

            return MapToResponse(mensalidade, aluno?.Nome, curso?.Nome);
        }

        public async Task<IEnumerable<TuitionResponse>> GetByReference(string referencia)
        {
            var mensalidades = await _repository.GetByReferenceAsync(referencia);
            var responses = new List<TuitionResponse>();

            foreach (var mensalidade in mensalidades)
            {
                var aluno = await _studentRepository.GetByIdAsync(mensalidade.AlunoId);
                var curso = await _courseRepository.GetByIdAsync(mensalidade.CursoId);
                responses.Add(MapToResponse(mensalidade, aluno?.Nome, curso?.Nome));
            }

            return responses;
        }

        public async Task<IEnumerable<TuitionResponse>> GetByStudent(Guid alunoId)
        {
            var aluno = await _studentRepository.GetByIdAsync(alunoId);
            if (aluno == null)
                throw new NotFoundException("Aluno não encontrado");

            var mensalidades = await _repository.GetByStudentAsync(alunoId);
            return mensalidades.Select(m => MapToResponse(m, aluno.Nome, m.Curso?.Nome));
        }

        public async Task ProcessPayment(Guid id, decimal valorPago, PaymentMethod formaPagamento)
        {
            var mensalidade = await _repository.GetByIdAsync(id);
            if (mensalidade == null)
                throw new NotFoundException("Mensalidade não encontrada");

            if (valorPago <= 0)
                throw new DomainException("Valor do pagamento deve ser maior que zero");

            await _repository.ProcessPaymentAsync(id, valorPago, formaPagamento);

            // Atualizar parcelas se existirem
            if (mensalidade.Parcelamentos?.Any() == true)
            {
                foreach (var parcela in mensalidade.Parcelamentos)
                {
                    await _installmentService.ProcessInstallmentPayment(parcela.Id);
                }
            }
        }

        public async Task<TuitionResponse> Update(Guid id, TuitionDTO dto)
        {
            var mensalidade = await _repository.GetByIdAsync(id);
            if (mensalidade == null)
                throw new NotFoundException("Mensalidade não encontrada");

            var aluno = await _studentRepository.GetByIdAsync(dto.AlunoId);
            if (aluno == null)
                throw new NotFoundException("Aluno não encontrado");

            var curso = await _courseRepository.GetByIdAsync(dto.CursoId);
            if (curso == null)
                throw new NotFoundException("Curso não encontrado");

            mensalidade.Update(
                dto.AlunoId,
                dto.CursoId,
                dto.Referencia,
                dto.Valor,
                dto.DataVencimento,
                mensalidade.StatusPagamento, // Mantém o status atual
                mensalidade.DataPagamento,   // Mantém a data de pagamento
                dto.FormaPagamento);

            await _repository.UpdateAsync(mensalidade);

            return MapToResponse(mensalidade, aluno.Nome, curso.Nome);
        }

        private async Task CreateInstallments(Tuition tuition, TuitionDTO dto)
        {
            decimal valorParcela = tuition.Valor / dto.NumeroParcelas;
            DateTime dataVencimento = tuition.DataVencimento;

            for (int i = 1; i <= dto.NumeroParcelas; i++)
            {
                var installmentDto = new InstallmentDTO
                {
                    MensalidadeId = tuition.Id,
                    ValorParcela = valorParcela,
                    DataVencimento = dataVencimento
                };

                await _installmentService.Create(installmentDto);
                dataVencimento = dataVencimento.AddMonths(1);
            }
        }

        private TuitionResponse MapToResponse(Tuition tuition, string alunoNome, string cursoNome)
        {
            return new TuitionResponse
            {
                Id = tuition.Id,
                AlunoId = tuition.AlunoId,
                NomeAluno = alunoNome,
                CursoId = tuition.CursoId,
                NomeCurso = cursoNome,
                Referencia = tuition.Referencia,
                Valor = tuition.Valor,
                DataVencimento = tuition.DataVencimento,
                StatusPagamento = tuition.StatusPagamento,
                DataPagamento = tuition.DataPagamento,
                FormaPagamento = tuition.FormaPagamento,
                DataCriacao = tuition.DataCriacao,
                TotalParcelas = tuition.Parcelamentos?.Count ?? 0,
                ValorPago = tuition.Parcelamentos?
                    .Where(p => p.Pago)
                    .Sum(p => p.ValorParcela) ?? 0
            };
        }
    }
}
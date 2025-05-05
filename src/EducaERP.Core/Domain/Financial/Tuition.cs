using EducaERP.Core.Domain.Academics;
using EducaERP.Core.Domain.Common;
using EducaERP.Core.Domain.Students;
using EducaERP.Core.Enums;

namespace EducaERP.Core.Domain.Financial
{
    public class Tuition : BaseEntity
    {
        public Guid AlunoId { get; private set; }
        public Guid CursoId { get; private set; }
        public string Referencia { get; private set; }
        public decimal Valor { get; private set; }
        public DateTime DataVencimento { get; private set; }
        public PaymentStatus StatusPagamento { get; set; }
        public DateTime DataPagamento { get; set; }
        public PaymentMethod FormaPagamento { get; set; }

        // Navigation properties
        public virtual Student? Aluno { get; private set; }
        public virtual Course? Curso { get; private set; }
        public virtual ICollection<Installment>? Parcelamentos { get; private set; } = new List<Installment>();

        protected Tuition() { }
        public Tuition(Guid alunoId, Guid cursoId, string referencia, decimal valor,
                      DateTime dataVencimento, PaymentStatus statusPagamento, DateTime dataPagamento,
                      PaymentMethod formaPagamento)
        {
            AlunoId = alunoId;
            CursoId = cursoId;
            Referencia = referencia;
            Valor = valor;
            DataVencimento = dataVencimento;
            StatusPagamento = statusPagamento;
            DataPagamento = dataPagamento;
            FormaPagamento = formaPagamento;

            Validate();
        }

        // Método para atualização
        public void Update(Guid alunoId, Guid cursoId, string referencia, decimal valor,
                      DateTime dataVencimento, PaymentStatus statusPagamento, DateTime dataPagamento,
                      PaymentMethod formaPagamento)
        {
            AlunoId = alunoId;
            CursoId = cursoId;
            Referencia = referencia;
            Valor = valor;
            DataVencimento = dataVencimento;
            StatusPagamento = statusPagamento;
            DataPagamento = dataPagamento;
            FormaPagamento = formaPagamento;
            UpdateTimestamp();

            Validate();
        }

        private void Validate()
        {
            if (AlunoId == Guid.Empty)
                throw new DomainException("ID do professor é inválido");

            if (CursoId == Guid.Empty)
                throw new DomainException("ID do Curso é inválido");

            if (string.IsNullOrWhiteSpace(Referencia))
                throw new DomainException("Referencia da mensalidade é obrigatório");

            if (Valor < 0)
                throw new DomainException("Valor da mensalidade não pode ser negativo");

            if (Valor <= 0)
                throw new DomainException("Valor da mensalidade deve ser maior que zero");

            //if (DataVencimento > DateTime.UtcNow)
            //    throw new DomainException("Data de vencimento não pode ser no futuro");

            //if (DataPagamento > DateTime.UtcNow)
            //    throw new DomainException("Data de vencimento não pode ser no futuro");
        }
    }
}

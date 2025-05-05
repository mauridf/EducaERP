using System.Drawing;
using EducaERP.Core.Domain.Common;
using EducaERP.Core.Enums;

namespace EducaERP.Core.Domain.Financial
{
    public class Installment : BaseEntity
    {
        public Guid MensalidadeId { get; private set; }
        public int ParcelaNumero { get; private set; }
        public decimal ValorParcela { get; private set; }
        public DateTime DataVencimento { get; private set; }
        public DateTime? DataPagamento { get; set; }
        public bool Pago { get; set; }

        // Navigation properties
        public virtual Tuition? Mensalidade { get; private set; }

        public Installment(Guid mensalidadeId, int parcelaNumero, decimal valorParcela, DateTime dataVencimento, bool pago)
        {
            MensalidadeId = mensalidadeId;
            ParcelaNumero = parcelaNumero;
            ValorParcela = valorParcela;
            DataVencimento = dataVencimento;
            Pago = pago;

            Validate();
        }

        // Método para atualização
        public void Update(Guid mensalidadeId, int parcelaNumero, decimal valorParcela, DateTime dataVencimento, bool pago)
        {
            MensalidadeId = mensalidadeId;
            ParcelaNumero = parcelaNumero;
            ValorParcela = valorParcela;
            DataVencimento = dataVencimento;
            Pago = pago;
            UpdateTimestamp();

            Validate();
        }

        private void Validate()
        {
            if (MensalidadeId == Guid.Empty)
                throw new DomainException("ID da mensalidade é inválido");

            if (ParcelaNumero <= 0)
                throw new DomainException("Parcela deve ser maior que zero");

            if (ValorParcela <= 0)
                throw new DomainException("Valor da parcela deve ser maior que zero");

            //if (DataVencimento > DateTime.UtcNow)
            //    throw new DomainException("Data de vencimento não pode ser no futuro");
        }
    }
}

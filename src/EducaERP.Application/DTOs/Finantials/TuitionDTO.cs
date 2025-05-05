using EducaERP.Core.Enums;

namespace EducaERP.Application.DTOs.Financial
{
    public class TuitionDTO
    {
        public Guid AlunoId { get; set; }
        public Guid CursoId { get; set; }
        public string Referencia { get; set; }
        public decimal Valor { get; set; }
        public DateTime DataVencimento { get; set; }
        public PaymentMethod FormaPagamento { get; set; }
        public int NumeroParcelas { get; set; } = 1;
    }
}
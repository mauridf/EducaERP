using EducaERP.Core.Enums;

namespace EducaERP.Application.DTOs.Financial.Responses
{
    public class TuitionResponse
    {
        public Guid Id { get; set; }
        public Guid AlunoId { get; set; }
        public string NomeAluno { get; set; }
        public Guid CursoId { get; set; }
        public string NomeCurso { get; set; }
        public string Referencia { get; set; }
        public decimal Valor { get; set; }
        public DateTime DataVencimento { get; set; }
        public PaymentStatus StatusPagamento { get; set; }
        public string StatusPagamentoDescricao => StatusPagamento.ToString();
        public DateTime? DataPagamento { get; set; }
        public PaymentMethod FormaPagamento { get; set; }
        public string FormaPagamentoDescricao => FormaPagamento.ToString();
        public DateTime DataCriacao { get; set; }
        public int TotalParcelas { get; set; }
        public decimal ValorPago { get; set; }
    }
}
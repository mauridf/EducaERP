namespace EducaERP.Application.DTOs.Financial.Responses
{
    public class InstallmentResponse
    {
        public Guid Id { get; set; }
        public Guid MensalidadeId { get; set; }
        public int ParcelaNumero { get; set; }
        public decimal ValorParcela { get; set; }
        public DateTime DataVencimento { get; set; }
        public bool Pago { get; set; }
        public DateTime? DataPagamento { get; set; }
    }
}
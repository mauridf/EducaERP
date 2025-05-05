namespace EducaERP.Application.DTOs.Financial
{
    public class InstallmentDTO
    {
        public Guid MensalidadeId { get; set; }
        public decimal ValorParcela { get; set; }
        public DateTime DataVencimento { get; set; }
    }
}
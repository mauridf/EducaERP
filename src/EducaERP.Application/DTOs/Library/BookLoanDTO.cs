using EducaERP.Core.Enums;

namespace EducaERP.Application.DTOs.Library
{
    public class BookLoanDTO
    {
        public Guid LivroId { get; set; }
        public Guid? AlunoId { get; set; }
        public Guid? ProfessorId { get; set; }
        public Guid? FuncionarioId { get; set; }
        public DateTime DataPrevistaDevolucao { get; set; }
    }
}
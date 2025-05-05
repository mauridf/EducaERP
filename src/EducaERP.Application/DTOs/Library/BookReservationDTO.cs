namespace EducaERP.Application.DTOs.Library
{
    public class BookReservationDTO
    {
        public Guid LivroId { get; set; }
        public Guid? AlunoId { get; set; }
        public Guid? ProfessorId { get; set; }
        public Guid? FuncionarioId { get; set; }
    }
}
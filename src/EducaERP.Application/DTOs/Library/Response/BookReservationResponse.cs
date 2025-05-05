using EducaERP.Core.Enums;

namespace EducaERP.Application.DTOs.Library.Responses
{
    public class BookReservationResponse
    {
        public Guid Id { get; set; }
        public Guid LivroId { get; set; }
        public string TituloLivro { get; set; }
        public Guid? AlunoId { get; set; }
        public string NomeAluno { get; set; }
        public Guid? ProfessorId { get; set; }
        public string NomeProfessor { get; set; }
        public Guid? FuncionarioId { get; set; }
        public string NomeFuncionario { get; set; }
        public DateTime DataReserva { get; set; }
        public ReservationStatus Status { get; set; }
        public string StatusDescricao => Status.ToString();
    }
}
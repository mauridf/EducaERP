using EducaERP.Core.Enums;

namespace EducaERP.Application.DTOs.Enrollments.Responses
{
    public class EnrollmentResponse
    {
        public Guid Id { get; set; }
        public Guid AlunoId { get; set; }
        public string NomeAluno { get; set; }
        public Guid CursoId { get; set; }
        public string NomeCurso { get; set; }
        public string AnoLetivo { get; set; }
        public string PeriodoLetivo { get; set; }
        public EnrollmentStatus Status { get; set; }
        public string StatusDescricao => Status.ToString();
        public DateTime DataCriacao { get; set; }
        public DateTime? DataAtualizacao { get; set; }
    }
}
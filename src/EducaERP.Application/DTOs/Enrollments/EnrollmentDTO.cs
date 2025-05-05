using EducaERP.Core.Enums;

namespace EducaERP.Application.DTOs.Enrollments
{
    public class EnrollmentDTO
    {
        public Guid AlunoId { get; set; }
        public Guid CursoId { get; set; }
        public string AnoLetivo { get; set; }
        public string PeriodoLetivo { get; set; }
        public EnrollmentStatus Status { get; set; } = EnrollmentStatus.Ativa;
    }
}
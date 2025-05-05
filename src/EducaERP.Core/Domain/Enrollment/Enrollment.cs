using EducaERP.Core.Domain.Academics;
using EducaERP.Core.Domain.Common;
using EducaERP.Core.Domain.Students;
using EducaERP.Core.Enums;

namespace EducaERP.Core.Domain.Enrollments
{
    public class Enrollment : BaseEntity
    {
        public Guid AlunoId { get; private set; }
        public Guid CursoId { get; private set; }
        public string AnoLetivo { get; private set; }
        public string PeriodoLetivo { get; private set; }
        public EnrollmentStatus Status { get; set; }

        // Navigation properties
        public virtual Student? Aluno { get; private set; }
        public virtual Course? Curso { get; private set; }

        public Enrollment(Guid alunoId, Guid cursoId, string anoLetivo,
                      string periodoLetivo, EnrollmentStatus status)
        {
            AlunoId = alunoId;
            CursoId = cursoId;
            AnoLetivo = anoLetivo;
            PeriodoLetivo = periodoLetivo;
            Status = status;

            Validate();
        }

        // Método para atualização
        public void Update(Guid alunoId, Guid cursoId, string anoLetivo,
                      string periodoLetivo, EnrollmentStatus status)
        {
            AlunoId = alunoId;
            CursoId = cursoId;
            AnoLetivo = anoLetivo;
            PeriodoLetivo = periodoLetivo;
            Status = status;
            UpdateTimestamp();

            Validate();
        }

        private void Validate()
        {
            if (AlunoId == Guid.Empty)
                throw new DomainException("ID do aluno é inválido");

            if (CursoId == Guid.Empty)
                throw new DomainException("ID do curso é inválido");

            if (string.IsNullOrWhiteSpace(AnoLetivo))
                throw new DomainException("Ano Letivo da matrícula é obrigatório");

            if (string.IsNullOrWhiteSpace(PeriodoLetivo))
                throw new DomainException("Período Letivo da matrícula é obrigatório");
        }
    }
}

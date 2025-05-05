using EducaERP.Core.Domain.Common;
using EducaERP.Core.Domain.Students;
using EducaERP.Core.Enums;

namespace EducaERP.Core.Domain.Academics
{
    public class Attendance : BaseEntity
    {
        public Guid AlunoId { get; private set; }
        public Guid DisciplinaId { get; private set; }
        public DateTime DataAula { get; private set; }
        public AttendanceStatus Presenca { get; private set; }
        public string Observacoes { get; private set; }

        // Navigation properties
        public virtual Student? Aluno { get; private set; }
        public virtual Discipline? Disciplina { get; private set; }

        public Attendance(Guid alunoId, Guid disciplinaId, DateTime dataAula, AttendanceStatus presenca,
                      string observacoes)
        {
            AlunoId = alunoId;
            DisciplinaId = disciplinaId;
            DataAula = dataAula;
            Presenca = presenca;
            Observacoes = observacoes;

            Validate();
        }

        // Método para atualização
        public void Update(Guid alunoId, Guid disciplinaId, DateTime dataAula, AttendanceStatus presenca,
                      string observacoes)
        {
            AlunoId = alunoId;
            DisciplinaId = disciplinaId;
            DataAula = dataAula;
            Presenca = presenca;
            Observacoes = observacoes;
            UpdateTimestamp();

            Validate();
        }

        private void Validate()
        {
            if (AlunoId == Guid.Empty)
                throw new DomainException("ID do professor é inválido");

            if (DisciplinaId == Guid.Empty)
                throw new DomainException("ID da disciplina é inválido");

            if (DataAula > DateTime.UtcNow)
                throw new DomainException("Data da aula não pode ser no futuro");

            if (string.IsNullOrWhiteSpace(Observacoes))
                throw new DomainException("Observação é obrigatório");
        }
    }
}

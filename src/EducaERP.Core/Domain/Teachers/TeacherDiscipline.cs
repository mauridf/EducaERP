using EducaERP.Core.Domain.Academics;
using EducaERP.Core.Domain.Common;

namespace EducaERP.Core.Domain.Teachers
{
    public class TeacherDiscipline : BaseEntity
    {
        public Guid ProfessorId { get; private set; }
        public Guid DisciplinaId { get; private set; }
        public bool EhResponsavel { get; private set; } // Se é o professor responsável pela disciplina
        public DateTime DataInicio { get; private set; }
        public DateTime? DataFim { get; private set; }

        // Navigation properties
        public virtual Teacher? Professor { get; private set; }
        public virtual Discipline? Disciplina { get; private set; }

        // Construtor privado para o EF Core
        private TeacherDiscipline() { }

        public TeacherDiscipline(Guid professorId, Guid disciplinaId, bool ehResponsavel = false)
        {
            ProfessorId = professorId;
            DisciplinaId = disciplinaId;
            EhResponsavel = ehResponsavel;
            DataInicio = DateTime.UtcNow;

            Validate();
        }

        // Métodos de domínio
        public void TornarResponsavel()
        {
            EhResponsavel = true;
            UpdateTimestamp();
        }

        public void RemoverComoResponsavel()
        {
            EhResponsavel = false;
            UpdateTimestamp();
        }

        public void EncerrarVinculo()
        {
            DataFim = DateTime.UtcNow;
            UpdateTimestamp();
        }

        public bool EstaAtivo()
        {
            return DataFim == null || DataFim > DateTime.UtcNow;
        }

        private void Validate()
        {
            if (ProfessorId == Guid.Empty)
                throw new DomainException("ID do professor é inválido");

            if (DisciplinaId == Guid.Empty)
                throw new DomainException("ID da disciplina é inválido");

            if (DataFim.HasValue && DataFim < DataInicio)
                throw new DomainException("Data de fim não pode ser anterior à data de início");
        }
    }
}
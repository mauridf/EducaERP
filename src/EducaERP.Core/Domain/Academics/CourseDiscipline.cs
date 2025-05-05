using EducaERP.Core.Domain.Common;

namespace EducaERP.Core.Domain.Academics
{
    public class CourseDiscipline : BaseEntity  // Herdando de BaseEntity para ter ID e timestamps
    {
        public Guid CursoId { get; private set; }
        public Guid DisciplinaId { get; private set; }
        public int Ordem { get; private set; }  // Exemplo: ordem da disciplina no curso
        public bool Obrigatoria { get; private set; } = true;

        // Navigation properties
        public virtual Course? Curso { get; private set; }
        public virtual Discipline? Disciplina { get; private set; }

        private CourseDiscipline() { }  // Construtor privado para EF Core

        public CourseDiscipline(Guid cursoId, Guid disciplinaId, int ordem = 0, bool obrigatoria = true)
        {
            CursoId = cursoId;
            DisciplinaId = disciplinaId;
            Ordem = ordem;
            Obrigatoria = obrigatoria;

            Validate();
        }

        public void UpdateOrder(int novaOrdem)
        {
            if (novaOrdem <= 0)
                throw new DomainException("A ordem deve ser maior que zero");

            Ordem = novaOrdem;
        }

        public void SetAsOptional()
        {
            Obrigatoria = false;
        }

        public void SetAsRequired()
        {
            Obrigatoria = true;
        }

        public void ToggleRequirement()
        {
            Obrigatoria = !Obrigatoria;
        }

        private void Validate()
        {
            if (CursoId == Guid.Empty)
                throw new DomainException("ID do curso é inválido");

            if (DisciplinaId == Guid.Empty)
                throw new DomainException("ID da disciplina é inválido");

            if (Ordem < 0)
                throw new DomainException("Ordem da disciplina não pode ser negativa");
        }
    }
}
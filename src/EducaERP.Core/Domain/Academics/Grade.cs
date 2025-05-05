using EducaERP.Core.Domain.Common;
using EducaERP.Core.Domain.Students;
using EducaERP.Core.Enums;

namespace EducaERP.Core.Domain.Academics
{
    public class Grade : BaseEntity
    {
        public Guid AlunoId { get; private set; }
        public Guid DisciplinaId { get; private set; }
        public AssessmentType TipoAvaliacao { get; private set; }
        public string DescricaoAvaliacao { get; private set; }
        public DateTime DataAvaliacao { get; private set; }
        public decimal NotaObtida { get; private set; }
        public decimal Peso { get; private set; }

        // Navigation properties
        public virtual Student? Aluno { get; private set; }
        public virtual Discipline? Disciplina { get; private set; }

        public Grade() { }

        public Grade(Guid alunoId, Guid disciplinaId, AssessmentType tipoAvaliacao, string descricao,
                      DateTime dataAvaliacao, decimal notaObtida, decimal peso)
        {
            AlunoId = alunoId;
            DisciplinaId = disciplinaId;
            TipoAvaliacao = tipoAvaliacao;
            DescricaoAvaliacao = descricao;
            DataAvaliacao = dataAvaliacao;
            NotaObtida = notaObtida;
            Peso = peso;

            Validate();
        }

        // Método para atualização
        public void Update(Guid alunoId, Guid disciplinaId, AssessmentType tipoAvaliacao, string descricao,
                      DateTime dataAvaliacao, decimal notaObtida, decimal peso)
        {
            AlunoId = alunoId;
            DisciplinaId = disciplinaId;
            TipoAvaliacao = tipoAvaliacao;
            DescricaoAvaliacao = descricao;
            DataAvaliacao = dataAvaliacao;
            NotaObtida = notaObtida;
            Peso = peso;
            UpdateTimestamp();

            Validate();
        }

        private void Validate()
        {
            if (AlunoId == Guid.Empty)
                throw new DomainException("ID do professor é inválido");

            if (DisciplinaId == Guid.Empty)
                throw new DomainException("ID da disciplina é inválido");

            if (string.IsNullOrWhiteSpace(DescricaoAvaliacao))
                throw new DomainException("Descrição da Avaliação é obrigatório");

            if (DataAvaliacao > DateTime.UtcNow)
                throw new DomainException("Data da avaliação não pode ser no futuro");

            if (NotaObtida < 0)
                throw new DomainException("Nota não pode ser negativa");

            if (NotaObtida > 10) // Ou outro valor máximo conforme seu sistema
                throw new DomainException("Nota não pode ser maior que 10");

            if (decimal.Round(NotaObtida, 2) != NotaObtida)
                throw new DomainException("Nota deve ter no máximo 2 casas decimais");

            if (Peso <= 0)
                throw new DomainException("Peso deve ser maior que zero");

            //if (Peso > 1) // Se estiver usando porcentagem (0-1)
            //    throw new DomainException("Peso não pode ser maior que 1");
        }
    }
}

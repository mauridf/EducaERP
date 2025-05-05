using EducaERP.Core.Domain.Common;
using EducaERP.Core.Domain.Institutions;
using EducaERP.Core.Domain.Students;
using EducaERP.Core.Domain.Teachers;
using EducaERP.Core.Enums;

namespace EducaERP.Core.Domain.Library
{
    public class BookLoan : BaseEntity
    {
        public Guid LivroId { get; private set; }
        public Guid? AlunoId { get; private set; }
        public Guid? ProfessorId { get; private set; }
        public Guid? FuncionarioId { get; private set; }
        public DateTime DataEmprestimo { get; private set; }
        public DateTime DataPrevistaDevolucao { get; private set; }
        public DateTime? DataDevolucao { get; private set; }
        public LoanStatus Status { get; private set; }

        // Navigation properties
        public virtual Book? Livro { get; private set; }
        public virtual Student? Aluno { get; private set; }
        public virtual Teacher? Professor { get; private set; }
        public virtual Employee? Funcionario { get; private set; }

        protected BookLoan() { }

        public BookLoan(Guid livroId, Guid? alunoId, Guid? professorId, Guid? funcionarioId,
                      DateTime dataEmprestimo, DateTime dataPrevistaDevolucao,
                      DateTime? dataDevolucao, LoanStatus status)
        {
            LivroId = livroId;
            AlunoId = alunoId;
            ProfessorId = professorId;
            FuncionarioId = funcionarioId;
            DataEmprestimo = dataEmprestimo;
            DataPrevistaDevolucao = dataPrevistaDevolucao;
            DataDevolucao = dataDevolucao;
            Status = status;

            Validate();
        }

        public void Update(Guid livroId, Guid? alunoId, Guid? professorId, Guid? funcionarioId,
                         DateTime dataEmprestimo, DateTime dataPrevistaDevolucao,
                         DateTime? dataDevolucao, LoanStatus status)
        {
            LivroId = livroId;
            AlunoId = alunoId;
            ProfessorId = professorId;
            FuncionarioId = funcionarioId;
            DataEmprestimo = dataEmprestimo;
            DataPrevistaDevolucao = dataPrevistaDevolucao;
            DataDevolucao = dataDevolucao;
            Status = status;
            UpdateTimestamp();

            Validate();
        }

        public void RegistrarDevolucao(DateTime dataDevolucao)
        {
            DataDevolucao = dataDevolucao;
            Status = LoanStatus.Devolvido;
            UpdateTimestamp();
            ValidateDevolucao();
        }

        private void Validate()
        {
            if (LivroId == Guid.Empty)
                throw new DomainException("ID do livro é inválido");

            // Verifica se pelo menos um ID de usuário foi fornecido
            if (!AlunoId.HasValue && !ProfessorId.HasValue && !FuncionarioId.HasValue)
                throw new DomainException("Pelo menos um ID de usuário (aluno, professor ou funcionário) deve ser informado");
        }

        private void ValidateDevolucao()
        {
            if (DataDevolucao < DataEmprestimo)
                throw new DomainException("Data de devolução não pode ser anterior à data de empréstimo");
        }
    }
}
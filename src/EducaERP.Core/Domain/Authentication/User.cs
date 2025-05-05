using EducaERP.Core.Domain.Common;
using EducaERP.Core.Domain.Institutions;
using EducaERP.Core.Domain.Students;
using EducaERP.Core.Domain.Teachers;
using EducaERP.Core.Enums;

namespace EducaERP.Core.Domain.Authentication
{
    public class User : BaseEntity
    {
        public string Usuario { get; private set; }
        public string SenhaHash { get; private set; }
        public UserType TipoUsuario { get; private set; }

        // Optional foreign keys
        public Guid? AlunoId { get; private set; }
        public Guid? ProfessorId { get; private set; }
        public Guid? FuncionarioId { get; private set; }

        // EF Navigation properties
        public virtual Student? Aluno { get; private set; }
        public virtual Teacher? Professor { get; private set; }
        public virtual Employee? Funcionario { get; private set; }

        public User(string usuario, string senhaHash, UserType tipoUsuario,
                   Guid? alunoId = null, Guid? professorId = null, Guid? funcionarioId = null)
        {
            Usuario = usuario;
            SenhaHash = senhaHash;
            TipoUsuario = tipoUsuario;
            AlunoId = alunoId;
            ProfessorId = professorId;
            FuncionarioId = funcionarioId;
            DataCriacao = DateTime.UtcNow;

            Validate();
        }

        private void Validate()
        {
            if (string.IsNullOrWhiteSpace(Usuario))
                throw new DomainException("Nome de usuário é obrigatório");

            if (string.IsNullOrWhiteSpace(SenhaHash))
                throw new DomainException("Senha é obrigatória");

            // Validação específica para tipos de usuário
            switch (TipoUsuario)
            {
                case UserType.Aluno when !AlunoId.HasValue:
                    throw new DomainException("AlunoId é obrigatório para usuários do tipo Aluno");
                case UserType.Professor when !ProfessorId.HasValue:
                    throw new DomainException("ProfessorId é obrigatório para usuários do tipo Professor");
                case UserType.Funcionario when !FuncionarioId.HasValue:
                    throw new DomainException("FuncionarioId é obrigatório para usuários do tipo Funcionario");
            }
        }
    }
}
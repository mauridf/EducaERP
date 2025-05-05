using EducaERP.Core.Domain.Common;
using EducaERP.Core.Domain.Institutions;
using EducaERP.Core.Domain.Library;

namespace EducaERP.Core.Domain.Teachers
{
    public class Teacher : BaseEntity
    {
        public Guid InstituicaoId { get; private set; }
        public string Nome { get; private set; }
        public string Cpf { get; private set; }
        public string Email { get; private set; }
        public string Telefone { get; private set; }
        public string Endereco { get; private set; }
        public string Cidade { get; private set; }
        public string Uf { get; private set; }
        public string Titulacao { get; private set; }

        // Navigation properties
        public virtual Institution? Instituicao { get; private set; }
        public virtual ICollection<TeacherDiscipline>? Disciplinas { get; private set; } = new List<TeacherDiscipline>();
        public virtual ICollection<BookLoan>? Emprestimos { get; private set; } = new List<BookLoan>();
        public virtual ICollection<BookReservation>? ReservaLivros { get; private set; } = new List<BookReservation>();

        public Teacher(Guid instituicaoId, string nome, string cpf, string email,
                      string telefone, string endereco, string cidade, string uf,string titulacao)
        {
            InstituicaoId = instituicaoId;
            Nome = nome;
            Cpf = cpf;
            Email = email;
            Telefone = telefone;
            Endereco = endereco;
            Cidade = cidade;
            Uf = uf;
            Titulacao = titulacao;

            Validate();
        }

        // Método para atualização
        public void Update(Guid instituicaoId, string nome, string cpf, string email,
                      string telefone, string endereco, string cidade, string uf, string titulacao)
        {
            InstituicaoId = instituicaoId;
            Nome = nome;
            Cpf = cpf;
            Email = email;
            Telefone = telefone;
            Endereco = endereco;
            Cidade = cidade;
            Uf = uf;
            Titulacao = titulacao;
            UpdateTimestamp();

            Validate();
        }

        private void Validate()
        {
            if (string.IsNullOrWhiteSpace(Nome))
                throw new DomainException("Nome do professor é obrigatório");

            if (string.IsNullOrWhiteSpace(Cpf) || !CpfValidator.IsValid(Cpf))
                throw new DomainException("CPF inválido");

            if (string.IsNullOrWhiteSpace(Email))
                throw new DomainException("E-mail do professor é obrigatório");

            if (string.IsNullOrWhiteSpace(Telefone))
                throw new DomainException("Telefone do professor é obrigatório");

            if (string.IsNullOrWhiteSpace(Endereco))
                throw new DomainException("Endereço do professor é obrigatório");

            if (string.IsNullOrWhiteSpace(Cidade))
                throw new DomainException("Cidade do professor é obrigatório");

            if (string.IsNullOrWhiteSpace(Uf))
                throw new DomainException("UF do professor é obrigatório");

            if (string.IsNullOrWhiteSpace(Titulacao))
                throw new DomainException("Titulação do professor é obrigatório");
        }
    }
}

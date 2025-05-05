using EducaERP.Core.Domain.Academics;
using EducaERP.Core.Domain.Common;
using EducaERP.Core.Domain.Enrollments;
using EducaERP.Core.Domain.Financial;
using EducaERP.Core.Domain.Institutions;
using EducaERP.Core.Domain.Library;
using EducaERP.Core.Enums;
using System.Diagnostics;

namespace EducaERP.Core.Domain.Students
{
    public class Student : BaseEntity
    {
        public Guid InstituicaoId { get; private set; }
        public string Nome { get; private set; }
        public string Cpf { get; private set; }
        public string Email { get; private set; }
        public string Telefone { get; private set; }
        public string Endereco { get; private set; }
        public string Cidade { get; private set; }
        public string Uf { get; private set; }

        // Navigation properties
        public virtual Institution? Instituicao { get; private set; }
        public virtual ICollection<Enrollment>? Matriculas { get; private set; } = new List<Enrollment>();
        public virtual ICollection<Attendance>? Frequencias { get; private set; } = new List<Attendance>();
        public virtual ICollection<Grade>? Notas { get; private set; } = new List<Grade>();
        public virtual ICollection<Tuition>? Mensalidades { get; private set; } = new List<Tuition>();
        public virtual ICollection<BookLoan>? Emprestimos { get; private set; } = new List<BookLoan>();
        public virtual ICollection<BookReservation>? ReservaLivros { get; private set; } = new List<BookReservation>();

        public Student(Guid instituicaoId, string nome, string cpf, string email,
                      string telefone, string endereco, string cidade, string uf)
        {
            InstituicaoId = instituicaoId;
            Nome = nome;
            Cpf = cpf;
            Email = email;
            Telefone = telefone;
            Endereco = endereco;
            Cidade = cidade;
            Uf = uf;

            Validate();
        }

        // Método para atualização
        public void Update(Guid instituicaoId, string nome, string cpf, string email,
                      string telefone, string endereco, string cidade, string uf)
        {
            Nome = nome;
            Cpf = cpf;
            Email = email;
            Telefone = telefone;
            Endereco = endereco;
            Cidade = cidade;
            Uf = uf;
            UpdateTimestamp();

            Validate();
        }

        private void Validate()
        {
            if (string.IsNullOrWhiteSpace(Nome))
                throw new DomainException("Nome do aluno é obrigatório");

            if (string.IsNullOrWhiteSpace(Cpf) || !CpfValidator.IsValid(Cpf))
                throw new DomainException("CPF inválido");

            if (string.IsNullOrWhiteSpace(Email))
                throw new DomainException("E-mail do aluno é obrigatório");

            if (string.IsNullOrWhiteSpace(Telefone))
                throw new DomainException("Telefone do aluno é obrigatório");

            if (string.IsNullOrWhiteSpace(Endereco))
                throw new DomainException("Endereço do aluno é obrigatório");

            if (string.IsNullOrWhiteSpace(Cidade))
                throw new DomainException("Cidade do aluno é obrigatório");

            if (string.IsNullOrWhiteSpace(Uf))
                throw new DomainException("UF do aluno é obrigatório");
        }
    }
}
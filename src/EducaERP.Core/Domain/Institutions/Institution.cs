using EducaERP.Core.Domain.Common;
using EducaERP.Core.Domain.Students;
using EducaERP.Core.Domain.Teachers;

namespace EducaERP.Core.Domain.Institutions
{
    public class Institution : BaseEntity
    {
        public string Nome { get; private set; }
        public string Cnpj { get; private set; }
        public string Email { get; private set; }
        public string Telefone { get; private set; }
        public string Endereco { get; private set; }
        public string Cidade { get; private set; }
        public string Uf { get; private set; }

        // EF Navigation properties
        public virtual ICollection<Employee>? Funcionarios { get; private set; } = new List<Employee>();
        public virtual ICollection<Student>? Alunos { get; private set; } = new List<Student>();
        public virtual ICollection<Teacher>? Professores { get; private set; } = new List<Teacher>();

        public Institution(string nome, string cnpj, string email, string telefone,
                          string endereco, string cidade, string uf)
        {
            Nome = nome;
            Cnpj = cnpj;
            Email = email;
            Telefone = telefone;
            Endereco = endereco;
            Cidade = cidade;
            Uf = uf;

            Validate();
        }

        // Método para atualização
        public void Update(string nome, string email, string telefone, string endereco,
                          string cidade, string uf)
        {
            Nome = nome;
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
                throw new DomainException("O nome da instituição é obrigatório");

            if (string.IsNullOrWhiteSpace(Cnpj) || !CnpjValidator.IsValid(Cnpj))
                throw new DomainException("CNPJ inválido");

            if (string.IsNullOrWhiteSpace(Email))
                throw new DomainException("E-mail da instituição é obrigatório");

            if (string.IsNullOrWhiteSpace(Telefone))
                throw new DomainException("Telefone da instituição é obrigatório");

            if (string.IsNullOrWhiteSpace(Endereco))
                throw new DomainException("Endereço da instituição é obrigatório");

            if (string.IsNullOrWhiteSpace(Cidade))
                throw new DomainException("Cidade da instituição é obrigatório");

            if (string.IsNullOrWhiteSpace(Uf))
                throw new DomainException("UF da instituição é obrigatório");
        }
    }
}
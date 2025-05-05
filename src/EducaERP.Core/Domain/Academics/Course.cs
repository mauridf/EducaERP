using EducaERP.Core.Domain.Common;
using EducaERP.Core.Domain.Enrollments;
using EducaERP.Core.Enums;

namespace EducaERP.Core.Domain.Academics
{
    public class Course : BaseEntity
    {
        public string Nome { get; private set; }
        public string Codigo { get; private set; }
        public string Descricao { get; private set; }
        public int CargaHorariaTotal { get; private set; }
        public EducationLevel Nivel { get; private set; }
        public EducationModality Modalidade { get; private set; }
        public int DuracaoMeses { get; private set; }
        public bool Ativo { get; set; }

        // EF Navigation properties
        public virtual ICollection<CourseDiscipline>? Disciplinas { get; private set; } = new List<CourseDiscipline>();
        public virtual ICollection<Enrollment>? Matriculas { get; private set; } = new List<Enrollment>();

        public Course(string nome, string codigo, string descricao,
                      int cargaHorariaTotal, EducationLevel nivel, EducationModality modalidade, int duracaoMeses, bool ativo)
        {
            Nome = nome;
            Codigo = codigo;
            Descricao = descricao;
            CargaHorariaTotal = cargaHorariaTotal;
            Nivel = nivel;
            Modalidade = modalidade;
            DuracaoMeses = duracaoMeses;
            Ativo = ativo;

            Validate();
        }

        // Método para atualização
        public void Update(string nome, string codigo, string descricao,
                      int cargaHorariaTotal, EducationLevel nivel, EducationModality modalidade, int duracaoMeses, bool ativo)
        {
            Nome = nome;
            Codigo = codigo;
            Descricao = descricao;
            CargaHorariaTotal = cargaHorariaTotal;
            Nivel = nivel;
            Modalidade = modalidade;
            DuracaoMeses = duracaoMeses;
            Ativo = ativo;
            UpdateTimestamp();

            Validate();
        }

        private void Validate()
        {
            if (string.IsNullOrWhiteSpace(Nome))
                throw new DomainException("Nome do curso é obrigatório");

            if (string.IsNullOrWhiteSpace(Codigo))
                throw new DomainException("Código do curso é obrigatório");

            if (string.IsNullOrWhiteSpace(Descricao))
                throw new DomainException("Descrição do curso é obrigatório");

            if (CargaHorariaTotal <= 0)
                throw new DomainException("Carga horária do curso deve ser maior que zero");

            if (DuracaoMeses <= 0)
                throw new DomainException("Duração do curso em meses deve ser maior que zero");
        }
    }
}
using EducaERP.Core.Domain.Common;
using EducaERP.Core.Domain.Teachers;
using EducaERP.Core.Enums;

namespace EducaERP.Core.Domain.Academics
{
    public class Discipline : BaseEntity
    {
        public string Nome { get; private set; }
        public string Codigo { get; private set; }
        public string Descricao { get; private set; }
        public int CargaHoraria { get; private set; }
        public int Periodo { get; private set; }
        public bool Obrigatoria { get; private set; }
        public bool Ativo { get; set; }

        // Navigation properties
        public virtual ICollection<CourseDiscipline>? Cursos { get; private set; } = new List<CourseDiscipline>();
        public virtual ICollection<TeacherDiscipline>? Professores { get; private set; } = new List<TeacherDiscipline>();

        public Discipline(string nome, string codigo, string descricao,
                      int cargaHoraria, int periodo, bool obrigatoria, bool ativo)
        {
            Nome = nome;
            Codigo = codigo;
            Descricao = descricao;
            CargaHoraria = cargaHoraria;
            Periodo = periodo;
            Obrigatoria = obrigatoria;
            Ativo = ativo;

            Validate();
        }

        // Método para atualização
        public void Update(string nome, string codigo, string descricao,
                      int cargaHoraria, int periodo, bool obrigatoria, bool ativo)
        {
            Nome = nome;
            Codigo = codigo;
            Descricao = descricao;
            CargaHoraria = cargaHoraria;
            Periodo = periodo;
            Obrigatoria = obrigatoria;
            Ativo = ativo;
            UpdateTimestamp();

            Validate();
        }
        private void Validate()
        {
            if (string.IsNullOrWhiteSpace(Nome))
                throw new DomainException("Nome da disciplina é obrigatória");

            if (string.IsNullOrWhiteSpace(Codigo))
                throw new DomainException("Código da disciplina é obrigatória");

            if (string.IsNullOrWhiteSpace(Descricao))
                throw new DomainException("Descrição da disciplina é obrigatória");

            if (CargaHoraria <= 0)
                throw new DomainException("Carga horária da disciplina deve ser maior que zero");

            if (Periodo <= 0)
                throw new DomainException("Período da disciplina deve ser maior que zero");
        }
    }
}

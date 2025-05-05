using System;

namespace EducaERP.Application.DTOs.Academics.Responses
{
    public class GradeResponse
    {
        public Guid Id { get; set; }
        public Guid AlunoId { get; set; }
        public Guid DisciplinaId { get; set; }
        public string TipoAvaliacao { get; set; }
        public string DescricaoAvaliacao { get; set; }
        public DateTime DataAvaliacao { get; set; }
        public decimal NotaObtida { get; set; }
        public decimal Peso { get; set; }
        public string NomeAluno { get; set; }
        public string NomeDisciplina { get; set; }
    }
}
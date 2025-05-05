using System;
using EducaERP.Core.Enums;

namespace EducaERP.Application.DTOs.Academics
{
    public class GradeDTO
    {
        public Guid AlunoId { get; set; }
        public Guid DisciplinaId { get; set; }
        public AssessmentType TipoAvaliacao { get; set; }
        public string DescricaoAvaliacao { get; set; }
        public DateTime DataAvaliacao { get; set; }
        public decimal NotaObtida { get; set; }
        public decimal Peso { get; set; }
    }
}
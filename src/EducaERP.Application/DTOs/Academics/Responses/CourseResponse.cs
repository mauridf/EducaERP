using System.Collections.Generic;
using EducaERP.Core.Enums;

namespace EducaERP.Application.DTOs.Academics.Responses
{
    public class CourseResponse
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Codigo { get; set; }
        public string Descricao { get; set; }
        public int CargaHorariaTotal { get; set; }
        public string Nivel { get; set; }
        public string Modalidade { get; set; }
        public int DuracaoMeses { get; set; }
        public bool Ativo { get; set; }
        public int TotalDisciplinas { get; set; }
        public int TotalMatriculas { get; set; }
        public IEnumerable<CourseDisciplineResponse> Disciplinas { get; set; }
    }
}
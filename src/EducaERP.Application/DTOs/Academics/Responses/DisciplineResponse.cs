using System.Collections.Generic;

namespace EducaERP.Application.DTOs.Academics.Responses
{
    public class DisciplineResponse
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Codigo { get; set; }
        public string Descricao { get; set; }
        public int CargaHoraria { get; set; }
        public int Periodo { get; set; }
        public bool Obrigatoria { get; set; }
        public bool Ativo { get; set; }
        public int TotalCursos { get; set; }
        public int TotalProfessores { get; set; }
    }
}
using EducaERP.Core.Enums;

namespace EducaERP.Application.DTOs.Academics
{
    public class CourseDTO
    {
        public string Nome { get; set; }
        public string Codigo { get; set; }
        public string Descricao { get; set; }
        public int CargaHorariaTotal { get; set; }
        public EducationLevel Nivel { get; set; }
        public EducationModality Modalidade { get; set; }
        public int DuracaoMeses { get; set; }
        public bool Ativo { get; set; }
    }
}
namespace EducaERP.Application.DTOs.Academics
{
    public class DisciplineDTO
    {
        public string Nome { get; set; }
        public string Codigo { get; set; }
        public string Descricao { get; set; }
        public int CargaHoraria { get; set; }
        public int Periodo { get; set; }
        public bool Obrigatoria { get; set; }
        public bool Ativo { get; set; }
    }
}
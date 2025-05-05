namespace EducaERP.Application.DTOs.Academics
{
    public class CourseDisciplineDTO
    {
        public Guid CursoId { get; set; }
        public Guid DisciplinaId { get; set; }
        public int Ordem { get; set; }
        public bool Obrigatoria { get; set; }
    }
}
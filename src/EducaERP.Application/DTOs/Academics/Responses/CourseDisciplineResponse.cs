namespace EducaERP.Application.DTOs.Academics.Responses
{
    public class CourseDisciplineResponse
    {
        public Guid CursoId { get; set; }
        public Guid DisciplinaId { get; set; }
        public string NomeDisciplina { get; set; }
        public int Ordem { get; set; }
        public bool Obrigatoria { get; set; }
    }
}
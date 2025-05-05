namespace EducaERP.Application.DTOs.Teachers
{
    public class TeacherDTO
    {
        public Guid InstituicaoId { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string Endereco { get; set; }
        public string Cidade { get; set; }
        public string Uf { get; set; }
        public string Titulacao { get; set; }
    }

    public class TeacherDisciplineDTO
    {
        public Guid ProfessorId { get; set; }
        public Guid DisciplinaId { get; set; }
        public bool EhResponsavel { get; set; }
    }
}
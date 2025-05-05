namespace EducaERP.Application.DTOs.Teachers.Responses
{
    public class TeacherResponse
    {
        public Guid Id { get; set; }
        public Guid InstituicaoId { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string Endereco { get; set; }
        public string Cidade { get; set; }
        public string Uf { get; set; }
        public string Titulacao { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime? DataAtualizacao { get; set; }

        // Relacionamentos (opcional)
        public string NomeInstituicao { get; set; }
        public int TotalDisciplinas { get; set; }
        public int TotalEmprestimos { get; set; }
        public IEnumerable<TeacherDisciplineResponse> Disciplinas { get; set; }
    }

    public class TeacherDisciplineResponse
    {
        public Guid DisciplinaId { get; set; }
        public string NomeDisciplina { get; set; }
        public bool EhResponsavel { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
        public bool EstaAtivo { get; set; }
    }
}
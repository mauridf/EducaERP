namespace EducaERP.Application.DTOs.Institutions.Responses
{
    public class InstitutionResponse
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Cnpj { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string Endereco { get; set; }
        public string Cidade { get; set; }
        public string Uf { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime? DataAtualizacao { get; set; }

        // Relacionamentos (opcional - dependendo das necessidades da API)
        public int TotalFuncionarios { get; set; }
        public int TotalAlunos { get; set; }
        public int TotalProfessores { get; set; }
    }
}
using EducaERP.Core.Enums;

namespace EducaERP.Application.DTOs.Institutions
{
    public class EmployeeDTO
    {
        public Guid InstituicaoId { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string Endereco { get; set; }
        public string Cidade { get; set; }
        public string Uf { get; set; }
        public EmployeePosition Cargo { get; set; }
    }
}
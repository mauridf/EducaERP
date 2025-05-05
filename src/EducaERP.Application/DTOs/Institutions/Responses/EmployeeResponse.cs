using System;
using EducaERP.Core.Enums;

namespace EducaERP.Application.DTOs.Institutions.Responses
{
    public class EmployeeResponse
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
        public EmployeePosition Cargo { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime? DataAtualizacao { get; set; }

        // Relacionamentos (opcional)
        public string NomeInstituicao { get; set; }
        public int TotalEmprestimos { get; set; }
        public int TotalReservas { get; set; }
    }
}
using System;
using System.Collections.Generic;

namespace EducaERP.Application.DTOs.Students.Responses
{
    public class StudentResponse
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
        public DateTime DataCriacao { get; set; }
        public DateTime? DataAtualizacao { get; set; }

        // Relacionamentos (opcional)
        public string NomeInstituicao { get; set; }
        public int TotalMatriculas { get; set; }
        public int TotalMensalidades { get; set; }
        public int TotalEmprestimos { get; set; }
    }
}
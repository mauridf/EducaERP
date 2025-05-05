using System;

namespace EducaERP.Application.DTOs.Academics.Responses
{
    public class AttendanceResponse
    {
        public Guid Id { get; set; }
        public Guid AlunoId { get; set; }
        public Guid DisciplinaId { get; set; }
        public DateTime DataAula { get; set; }
        public string Presenca { get; set; }
        public string Observacoes { get; set; }
        public string NomeAluno { get; set; }
        public string NomeDisciplina { get; set; }
        public DateTime DataCriacao { get; set; }
    }
}
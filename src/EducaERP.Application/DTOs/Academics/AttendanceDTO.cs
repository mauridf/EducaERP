using System;
using EducaERP.Core.Enums;

namespace EducaERP.Application.DTOs.Academics
{
    public class AttendanceDTO
    {
        public Guid AlunoId { get; set; }
        public Guid DisciplinaId { get; set; }
        public DateTime DataAula { get; set; }
        public AttendanceStatus Presenca { get; set; }
        public string Observacoes { get; set; }
    }
}
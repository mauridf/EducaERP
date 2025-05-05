using EducaERP.Application.DTOs.Academics;
using FluentValidation;
using System;

namespace EducaERP.Application.Validators.Academics
{
    public class AttendanceValidator : AbstractValidator<AttendanceDTO>
    {
        public AttendanceValidator()
        {
            RuleFor(x => x.AlunoId)
                .NotEmpty().WithMessage("ID do aluno é obrigatório");

            RuleFor(x => x.DisciplinaId)
                .NotEmpty().WithMessage("ID da disciplina é obrigatório");

            RuleFor(x => x.DataAula)
                .NotEmpty().WithMessage("Data da aula é obrigatória")
                .LessThanOrEqualTo(DateTime.Today).WithMessage("Data da aula não pode ser no futuro");

            RuleFor(x => x.Observacoes)
                .NotEmpty().WithMessage("Observações são obrigatórias")
                .MaximumLength(500).WithMessage("Observações devem ter no máximo 500 caracteres");
        }
    }
}
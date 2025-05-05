using EducaERP.Application.DTOs.Academics;
using FluentValidation;
using System;

namespace EducaERP.Application.Validators.Academics
{
    public class GradeValidator : AbstractValidator<GradeDTO>
    {
        public GradeValidator()
        {
            RuleFor(x => x.AlunoId)
                .NotEmpty().WithMessage("ID do aluno é obrigatório");

            RuleFor(x => x.DisciplinaId)
                .NotEmpty().WithMessage("ID da disciplina é obrigatório");

            RuleFor(x => x.DescricaoAvaliacao)
                .NotEmpty().WithMessage("Descrição da avaliação é obrigatória")
                .MaximumLength(200).WithMessage("Descrição deve ter no máximo 200 caracteres");

            RuleFor(x => x.DataAvaliacao)
                .NotEmpty().WithMessage("Data da avaliação é obrigatória")
                .LessThanOrEqualTo(DateTime.Today).WithMessage("Data da avaliação não pode ser no futuro");

            RuleFor(x => x.NotaObtida)
                .InclusiveBetween(0, 10).WithMessage("Nota deve estar entre 0 e 10");

            RuleFor(x => x.Peso)
                .GreaterThan(0).WithMessage("Peso deve ser maior que zero");
        }
    }
}
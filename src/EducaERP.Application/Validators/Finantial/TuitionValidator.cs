using EducaERP.Application.DTOs.Financial;
using FluentValidation;

namespace EducaERP.Application.Validators.Financial
{
    public class TuitionValidator : AbstractValidator<TuitionDTO>
    {
        public TuitionValidator()
        {
            RuleFor(x => x.AlunoId)
                .NotEmpty().WithMessage("ID do aluno é obrigatório");

            RuleFor(x => x.CursoId)
                .NotEmpty().WithMessage("ID do curso é obrigatório");

            RuleFor(x => x.Referencia)
                .NotEmpty().WithMessage("Referência é obrigatória")
                .MaximumLength(20).WithMessage("Referência deve ter no máximo 20 caracteres");

            RuleFor(x => x.Valor)
                .GreaterThan(0).WithMessage("Valor deve ser maior que zero");

            RuleFor(x => x.NumeroParcelas)
                .GreaterThan(0).WithMessage("Número de parcelas deve ser maior que zero")
                .LessThanOrEqualTo(12).WithMessage("Número máximo de parcelas é 12");
        }
    }
}
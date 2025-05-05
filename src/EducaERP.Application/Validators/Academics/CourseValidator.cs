using EducaERP.Application.DTOs.Academics;
using FluentValidation;

namespace EducaERP.Application.Validators.Academics
{
    public class CourseValidator : AbstractValidator<CourseDTO>
    {
        public CourseValidator()
        {
            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("Nome do curso é obrigatório")
                .MaximumLength(100).WithMessage("Nome deve ter no máximo 100 caracteres");

            RuleFor(x => x.Codigo)
                .NotEmpty().WithMessage("Código do curso é obrigatório")
                .MaximumLength(20).WithMessage("Código deve ter no máximo 20 caracteres");

            RuleFor(x => x.Descricao)
                .NotEmpty().WithMessage("Descrição do curso é obrigatória")
                .MaximumLength(500).WithMessage("Descrição deve ter no máximo 500 caracteres");

            RuleFor(x => x.CargaHorariaTotal)
                .GreaterThan(0).WithMessage("Carga horária deve ser maior que zero");

            RuleFor(x => x.DuracaoMeses)
                .GreaterThan(0).WithMessage("Duração em meses deve ser maior que zero");
        }
    }
}
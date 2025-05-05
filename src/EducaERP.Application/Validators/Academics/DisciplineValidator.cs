using EducaERP.Application.DTOs.Academics;
using FluentValidation;

namespace EducaERP.Application.Validators.Academics
{
    public class DisciplineValidator : AbstractValidator<DisciplineDTO>
    {
        public DisciplineValidator()
        {
            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("Nome da disciplina é obrigatório")
                .MaximumLength(100).WithMessage("Nome deve ter no máximo 100 caracteres");

            RuleFor(x => x.Codigo)
                .NotEmpty().WithMessage("Código da disciplina é obrigatório")
                .MaximumLength(20).WithMessage("Código deve ter no máximo 20 caracteres");

            RuleFor(x => x.Descricao)
                .NotEmpty().WithMessage("Descrição da disciplina é obrigatória")
                .MaximumLength(500).WithMessage("Descrição deve ter no máximo 500 caracteres");

            RuleFor(x => x.CargaHoraria)
                .GreaterThan(0).WithMessage("Carga horária deve ser maior que zero");

            RuleFor(x => x.Periodo)
                .GreaterThan(0).WithMessage("Período deve ser maior que zero");
        }
    }
}
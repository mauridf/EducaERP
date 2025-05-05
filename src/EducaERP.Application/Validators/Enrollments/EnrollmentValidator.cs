using EducaERP.Application.DTOs.Enrollments;
using FluentValidation;

namespace EducaERP.Application.Validators.Enrollments
{
    public class EnrollmentValidator : AbstractValidator<EnrollmentDTO>
    {
        public EnrollmentValidator()
        {
            RuleFor(x => x.AlunoId)
                .NotEmpty().WithMessage("ID do aluno é obrigatório");

            RuleFor(x => x.CursoId)
                .NotEmpty().WithMessage("ID do curso é obrigatório");

            RuleFor(x => x.AnoLetivo)
                .NotEmpty().WithMessage("Ano letivo é obrigatório")
                .Length(4, 10).WithMessage("Ano letivo deve ter entre 4 e 10 caracteres");

            RuleFor(x => x.PeriodoLetivo)
                .NotEmpty().WithMessage("Período letivo é obrigatório")
                .MaximumLength(20).WithMessage("Período letivo deve ter no máximo 20 caracteres");
        }
    }
}
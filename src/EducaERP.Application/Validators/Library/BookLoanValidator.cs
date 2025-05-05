using EducaERP.Application.DTOs.Library;
using FluentValidation;

namespace EducaERP.Application.Validators.Library
{
    public class BookLoanValidator : AbstractValidator<BookLoanDTO>
    {
        public BookLoanValidator()
        {
            RuleFor(x => x.LivroId)
                .NotEmpty().WithMessage("ID do livro é obrigatório");

            RuleFor(x => x.DataPrevistaDevolucao)
                .GreaterThan(DateTime.Now).WithMessage("Data prevista de devolução deve ser no futuro")
                .LessThan(DateTime.Now.AddMonths(6)).WithMessage("Prazo máximo de empréstimo é 6 meses");

            RuleFor(x => x)
                .Must(x => x.AlunoId.HasValue || x.ProfessorId.HasValue || x.FuncionarioId.HasValue)
                .WithMessage("Pelo menos um ID de usuário (aluno, professor ou funcionário) deve ser informado");
        }
    }
}
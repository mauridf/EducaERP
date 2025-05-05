using EducaERP.Application.DTOs.Library;
using FluentValidation;

namespace EducaERP.Application.Validators.Library
{
    public class BookReservationValidator : AbstractValidator<BookReservationDTO>
    {
        public BookReservationValidator()
        {
            RuleFor(x => x.LivroId)
                .NotEmpty().WithMessage("ID do livro é obrigatório");

            RuleFor(x => x)
                .Must(x => x.AlunoId.HasValue || x.ProfessorId.HasValue || x.FuncionarioId.HasValue)
                .WithMessage("Pelo menos um ID de usuário (aluno, professor ou funcionário) deve ser informado");
        }
    }
}
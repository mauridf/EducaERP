using EducaERP.Application.DTOs.Library;
using FluentValidation;

namespace EducaERP.Application.Validators.Library
{
    public class BookValidator : AbstractValidator<BookDTO>
    {
        public BookValidator()
        {
            RuleFor(x => x.Titulo)
                .NotEmpty().WithMessage("Título é obrigatório")
                .MaximumLength(200).WithMessage("Título deve ter no máximo 200 caracteres");

            RuleFor(x => x.Autor)
                .NotEmpty().WithMessage("Autor é obrigatório")
                .MaximumLength(100).WithMessage("Autor deve ter no máximo 100 caracteres");

            RuleFor(x => x.Editora)
                .NotEmpty().WithMessage("Editora é obrigatória")
                .MaximumLength(100).WithMessage("Editora deve ter no máximo 100 caracteres");

            RuleFor(x => x.AnoPublicacao)
                .GreaterThan(0).WithMessage("Ano de publicação deve ser maior que zero")
                .LessThanOrEqualTo(DateTime.Now.Year).WithMessage("Ano de publicação não pode ser no futuro");

            RuleFor(x => x.ISBN)
                .NotEmpty().WithMessage("ISBN é obrigatório")
                .Length(10, 13).WithMessage("ISBN deve ter entre 10 e 13 caracteres");

            RuleFor(x => x.QuantidadeTotal)
                .GreaterThan(0).WithMessage("Quantidade total deve ser maior que zero");
        }
    }
}
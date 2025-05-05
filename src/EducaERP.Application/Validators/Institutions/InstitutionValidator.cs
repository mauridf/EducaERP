using EducaERP.Application.DTOs.Institutions;
using FluentValidation;

namespace EducaERP.Application.Validators.Institutions
{
    public class InstitutionValidator : AbstractValidator<InstitutionDTO>
    {
        public InstitutionValidator()
        {
            RuleFor(x => x.Nome).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Cnpj).NotEmpty().Length(14);
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Telefone).NotEmpty();
            RuleFor(x => x.Endereco).NotEmpty();
            RuleFor(x => x.Cidade).NotEmpty();
            RuleFor(x => x.Uf).NotEmpty().Length(2);
        }
    }
}
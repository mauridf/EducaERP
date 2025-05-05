using EducaERP.Application.DTOs.Financial;
using FluentValidation;

namespace EducaERP.Application.Validators.Financial
{
    public class InstallmentValidator : AbstractValidator<InstallmentDTO>
    {
        public InstallmentValidator()
        {
            RuleFor(x => x.MensalidadeId)
                .NotEmpty().WithMessage("ID da mensalidade é obrigatório");

            RuleFor(x => x.ValorParcela)
                .GreaterThan(0).WithMessage("Valor da parcela deve ser maior que zero");
        }
    }
}
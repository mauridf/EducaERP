using EducaERP.Application.DTOs.Institutions;
using FluentValidation;
using System.Text.RegularExpressions;

namespace EducaERP.Application.Validators.Institutions
{
    public class EmployeeValidator : AbstractValidator<EmployeeDTO>
    {
        public EmployeeValidator()
        {
            RuleFor(x => x.InstituicaoId)
                .NotEmpty().WithMessage("ID da instituição é obrigatório");

            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("Nome é obrigatório")
                .MaximumLength(100).WithMessage("Nome deve ter no máximo 100 caracteres");

            RuleFor(x => x.Cpf)
                .NotEmpty().WithMessage("CPF é obrigatório")
                .Must(BeValidCpf).WithMessage("CPF inválido");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("E-mail é obrigatório")
                .EmailAddress().WithMessage("E-mail inválido");

            RuleFor(x => x.Telefone)
                .NotEmpty().WithMessage("Telefone é obrigatório");

            RuleFor(x => x.Endereco)
                .NotEmpty().WithMessage("Endereço é obrigatório");

            RuleFor(x => x.Cidade)
                .NotEmpty().WithMessage("Cidade é obrigatória");

            RuleFor(x => x.Uf)
                .NotEmpty().WithMessage("UF é obrigatória")
                .Length(2).WithMessage("UF deve ter 2 caracteres");

            RuleFor(x => x.Cargo)
                .IsInEnum().WithMessage("Cargo inválido");
        }

        private bool BeValidCpf(string cpf)
        {
            // Implementação da validação de CPF
            // Pode usar a mesma implementação que está no CpfValidator do domínio
            return !string.IsNullOrWhiteSpace(cpf) && cpf.Length == 11;
        }
    }
}
using API.Financeiro.Domain.Pessoa;
using FluentValidation;

namespace API.Financeiro.Business.Validators.Pessoa;

public class CreatePessoaValidator : AbstractValidator<CreatePessoa>
{
    public CreatePessoaValidator()
    {
        RuleFor(x => x.Nome)
            .NotNull().WithMessage("Faltando.")
            .NotEmpty().WithMessage("Deve ser informado.")
            .NotEqual("string").WithMessage("Inválido.");
    }
}

using API.Financeiro.Domain.Fornecedor;
using FluentValidation;

namespace API.Financeiro.Business.Validators.Fornecedor;

public class CreateFornecedorValidator : AbstractValidator<CreateFornecedor>
{
    public CreateFornecedorValidator()
    {
        RuleFor(x => x.Nome)
            .NotNull().WithMessage("Faltando.")
            .NotEmpty().WithMessage("Deve ser informado.")
            .NotEqual("string").WithMessage("Inválido.");
    }
}

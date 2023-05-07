using API.Financeiro.Domain.Cliente;
using FluentValidation;

namespace API.Financeiro.Business.Validators.Cliente;

public class CreateClienteValidator : AbstractValidator<CreateCliente>
{
    public CreateClienteValidator()
    {
        RuleFor(x => x.Nome)
            .NotNull().WithMessage("Faltando.")
            .NotEmpty().WithMessage("Deve ser informado.")
            .NotEqual("string").WithMessage("Inválido.");
    }
}

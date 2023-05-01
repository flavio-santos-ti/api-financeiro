using API.Financeiro.Domain.Categoria;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Financeiro.Business.Validators.Categoria;

public class CreateCategoriaValidator : AbstractValidator<CreateCategoria>
{
    public CreateCategoriaValidator()
    {
        RuleFor(x => x.Nome)
            .NotNull().WithMessage("Faltando.")
            .NotEmpty().WithMessage("Deve ser informado.")
            .NotEqual("string").WithMessage("Inválido.");

        RuleFor(x => x.Tipo).Must(ValidarTipo).WithMessage("Inválido.");
    }

    private bool ValidarTipo(string tipo)
    {
        if (tipo != "E")
        {
            if (tipo != "S")
            {
                return false;
            } else
            {
                return true;
            }
        } 
        else
        {
            return true;
        } 
    }

}

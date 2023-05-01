using API.Financeiro.Domain.Result;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace API.Financeiro.Business.Services.Base;

public class ServiceBase
{
    private string GetArtigoIndefinido(string substantivo)
    {
        string artigoIndefinido = substantivo.Substring(substantivo.Length - 1, 1) == "o" ? "um" : "uma";
        return artigoIndefinido;
    }

    private string GetUltimaLetra(string substantivo)
    {
        string ultimaLetra = substantivo.Substring(substantivo.Length - 1, 1) == "o" ? "o" : "a";
        return ultimaLetra;
    }

    protected ServiceResult ErrorJaExiste(string name)
    {
        var result = new ServiceResult();
        result.Successed = false;
        result.Name = name;
        result.Message = name + $" já cadastrad{this.GetUltimaLetra(name)}.";
        result.Data = null;
        return result;
    }

    protected ServiceResult ErrorDelete(string name)
    {
        var result = new ServiceResult();
        result.Successed = false;
        result.Name = name;
        result.Message = $"Erro ao tentar excluir {this.GetUltimaLetra(name)} {name}.";
        result.Data = null;
        return result;
    }

    protected ServiceResult ErrorNaoEncontrado(string name)
    {
        var result = new ServiceResult();
        result.Successed = false;
        result.Name = name;
        result.Message = name + $" não encontrad{this.GetUltimaLetra(name)}.";
        result.Data = null;
        return result;
    }

    protected ServiceResult ErrorValidationCreate(ValidationResult result, string name)
    {
        var erros = new List<ServiceValidationResult>();

        foreach (var error in result.Errors)
        {
            erros.Add(
                new ServiceValidationResult
                {
                    PropertyName = error.PropertyName,
                    ErrorMessage = error.ErrorMessage
                }
            );
        };

        ServiceResult resultError = new();
        resultError.Successed = false;
        resultError.Name = name;
        resultError.Message = $"Erro ao tentar criar {this.GetArtigoIndefinido(name)} " + name + ".";
        resultError.Data = erros;

        return resultError;
    }

    protected ServiceResult SuccessedAdd(object dados, string name)
    {
        string artigoIndefinido = this.GetArtigoIndefinido(name);

        var result = new ServiceResult();
        result.Successed = true;
        result.Name = name;
        result.Message = name + $" adicionad{this.GetUltimaLetra(name)} com sucesso.";
        result.Data = dados;
        return result;
    }

    protected ServiceResult SuccessedDelete(string name)
    {
        string artigoIndefinido = this.GetArtigoIndefinido(name);

        var result = new ServiceResult();
        result.Successed = true;
        result.Name = name;
        result.Message = name + $" excluid{this.GetUltimaLetra(name)} com sucesso.";
        result.Data = null;
        return result;
    }

    protected ServiceResult SuccessedViewAll(object dados, string name, int count)
    {
        string message = $"{count} registro(s) encontrado(s).";
        var result = new ServiceResult();
        result.Successed = true;
        result.Name = name;
        result.Message = message;
        result.Data = dados;
        return result;
    }

}

using API.Financeiro.Domain.Result;
using FluentValidation.Results;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace API.Financeiro.Business.Services.Base;

public class ServiceBase
{
    private string GetArtigoIndefinido(string substantivo)
    {
        string artigoIndefinido = substantivo.Substring(substantivo.Length - 1, 1) == "o" || substantivo.Substring(substantivo.Length - 1, 1) == "e" ? "um" : "uma";
        return artigoIndefinido;
    }

    private string GetUltimaLetra(string substantivo)
    {
        string ultimaLetra = substantivo.Substring(substantivo.Length - 1, 1);
        
        switch (ultimaLetra)
        {
            case "o":
                return "o";
            case "e":
                return "o";
            case "r":
                return "o";
            default:
                return "a";
        }
    }
    protected string ClearString(string text)
    {
        return new string(text
                .Normalize(NormalizationForm.FormD)
                .Where(ch => char.GetUnicodeCategory(ch) != UnicodeCategory.NonSpacingMark)
                .ToArray());
    }

    protected ServiceResult ErrorJaExiste(string name)
    {
        var result = new ServiceResult();
        result.TransactionId = Guid.NewGuid();
        result.TransactionName = name;
        result.Successed = false;
        result.Message = name + $" já cadastrad{this.GetUltimaLetra(name)}.";
        result.Data = null;
        return result;
    }

    protected ServiceResult Error(string message, string name)
    {
        var result = new ServiceResult();
        result.TransactionId = Guid.NewGuid();
        result.TransactionName = name;
        result.Successed = false;
        result.Message = message;
        result.Data = null;
        return result;
    }

    protected ServiceResult Successed(string message, string name)
    {
        string artigoIndefinido = this.GetArtigoIndefinido(name);

        var result = new ServiceResult();
        result.TransactionId = Guid.NewGuid();
        result.TransactionName = name;
        result.Successed = true;
        result.Message = message;
        result.Data = null;
        return result;
    }

    protected ServiceResult Successed(string message, string name, object dados, long id)
    {
        string artigoIndefinido = this.GetArtigoIndefinido(name);

        var result = new ServiceResult();
        result.TransactionId = Guid.NewGuid();
        result.TransactionName = name;
        result.Successed = true;
        result.Message = message;
        result.Data = dados;
        return result;
    }

    protected ServiceResult ErrorDelete(string name)
    {
        var result = new ServiceResult();
        result.TransactionId = Guid.NewGuid();
        result.TransactionName = name;
        result.Successed = false;
        result.Message = $"Erro ao tentar excluir {this.GetUltimaLetra(name)} {name}.";
        result.Data = null;
        return result;
    }

    protected ServiceResult ErrorNaoEncontrado(string name)
    {
        var result = new ServiceResult();
        result.TransactionId = Guid.NewGuid();
        result.TransactionName = name;
        result.Successed = false;
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
        resultError.TransactionId = Guid.NewGuid();
        resultError.TransactionName = name;
        resultError.Successed = false;
        resultError.Message = $"Erro ao tentar criar {this.GetArtigoIndefinido(name)} " + name + ".";
        resultError.Data = erros;

        return resultError;
    }

    protected ServiceResult SuccessedAdd(object dados, string name)
    {
        string artigoIndefinido = this.GetArtigoIndefinido(name);

        var result = new ServiceResult();
        result.TransactionId = Guid.NewGuid();
        result.TransactionName = name;
        result.Successed = true;
        result.Message = name + $" adicionad{this.GetUltimaLetra(name)} com sucesso.";
        result.Data = dados;
        result.Count = 1;
        return result;
    }
    protected ServiceResult SuccessedAddId(long id, string name)
    {
        string artigoIndefinido = this.GetArtigoIndefinido(name);

        var result = new ServiceResult();
        result.TransactionId = Guid.NewGuid();
        result.TransactionName = name;
        result.Successed = true;
        result.Message = name + $" adicionad{this.GetUltimaLetra(name)} com sucesso.";
        result.Data = null;
        return result;
    }

    protected ServiceResult SuccessedDelete(string name)
    {
        string artigoIndefinido = this.GetArtigoIndefinido(name);

        var result = new ServiceResult();
        result.TransactionId = Guid.NewGuid();
        result.TransactionName = name;
        result.Successed = true;
        result.Message = name + $" excluid{this.GetUltimaLetra(name)} com sucesso.";
        result.Data = null;
        return result;
    }

    protected ServiceResult SuccessedViewAll(object dados, string name, int count)
    {
        string message = $"{count} registro(s) encontrado(s).";
        var result = new ServiceResult();
        result.TransactionId = Guid.NewGuid();
        result.TransactionName = name;
        result.Count = count;
        result.Successed = true;
        result.Message = message;
        result.Data = dados;
        return result;
    }

    protected string GetHashMD5(string input)
    {
        using (MD5 md5Hash = System.Security.Cryptography.MD5.Create())
        {
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            StringBuilder sBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString().ToUpper();
        }
    }
}

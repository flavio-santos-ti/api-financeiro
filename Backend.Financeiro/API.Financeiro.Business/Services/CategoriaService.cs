﻿using Api.Crud.Infra.Data.Interfaces;
using API.Financeiro.Business.Interfaces;
using API.Financeiro.Business.Services.Base;
using API.Financeiro.Domain.Categoria;
using API.Financeiro.Domain.Result;
using API.Financeiro.Infra.Data.Interfaces;
using FluentValidation;
using FluentValidation.Results;

namespace API.Financeiro.Business.Services;

public class CategoriaService : ServiceBase, ICategoriaService
{
    private readonly IValidator<CreateCategoria> _validatorCreate;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICategoriaRepository _categoriaRepository;


    public CategoriaService(IValidator<CreateCategoria> validatorCreate, IUnitOfWork unitOfWork, ICategoriaRepository categoriaRepository)
    {
        _validatorCreate = validatorCreate;
        _unitOfWork = unitOfWork;
        _categoriaRepository = categoriaRepository;
    }

    public async Task<ServiceResult> CreateAsync(CreateCategoria dados)
    {
        ValidationResult result = await _validatorCreate.ValidateAsync(dados);

        if (!result.IsValid)
        {
            return base.ErrorValidationCreate(result, "Categoria");
        }

        await _unitOfWork.BeginTransactionAsync();

        var categoria = await _categoriaRepository.GetAsync(dados.Nome);

        if (categoria == null) 
        {
            Categoria newCategoria = new();
            newCategoria.Nome = dados.Nome;
            newCategoria.Tipo = dados.Tipo;
            await _categoriaRepository.AddAsync(newCategoria);
            await _unitOfWork.SaveAsync();
            await _unitOfWork.CommitAsync();
            return base.SuccessedAdd(newCategoria, "Categoria");
        }
        else
        {
            await _unitOfWork.RolbackAsync();
            return base.ErrorJaExiste("Categoria");
        }
    }

    public async Task<ServiceResult> DeleteAsync(long id)
    {
        await _unitOfWork.BeginTransactionAsync();

        var categoria = await _categoriaRepository.GetAsync(id);

        if (categoria == null)
        {
            await _unitOfWork.RolbackAsync();
            return base.ErrorNaoEncontrado("Categoria");
        } else
        {
            int result = await _categoriaRepository.DeleteAsync(categoria);
            await _unitOfWork.SaveAsync();
            
            if (result == 1)
            {
                await _unitOfWork.CommitAsync();
                return base.SuccessedDelete("Categoria");
            } else
            {
                await _unitOfWork.RolbackAsync();
                return base.ErrorDelete("Categoria");
            }
        }
    }

    public async Task<ServiceResult> GetViewAllAsync(int skip, int take)
    {
        var categorias = await _categoriaRepository.GetViewAllAsync(skip, take);

        return base.SuccessedViewAll(categorias, "Categoria", categorias.Count());
    }

}

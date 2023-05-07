using Api.Crud.Infra.Data.Interfaces;
using API.Financeiro.Business.Interfaces;
using API.Financeiro.Business.Services.Base;
using API.Financeiro.Domain.Cliente;
using API.Financeiro.Domain.Pessoa;
using API.Financeiro.Domain.Result;
using API.Financeiro.Infra.Data.Interfaces;
using API.Financeiro.Infra.Data.Repositories;

namespace API.Financeiro.Business.Services;

public class ClienteService : ServiceBase, IClienteService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPessoaService _pessoaService;
    private readonly IClienteRepository _clienteRepository;

    public ClienteService(IUnitOfWork unitOfWork, IPessoaService pessoaService, IClienteRepository clienteRepository)
    {
        _unitOfWork = unitOfWork;
        _pessoaService = pessoaService;
        _clienteRepository = clienteRepository;
    }

    public async Task<ServiceResult> CreateAsync(CreateCliente dados)
    {
        await _unitOfWork.BeginTransactionAsync();

        CreatePessoa newPessoa = new();
        newPessoa.Nome= dados.Nome;

        var pessoa = await _pessoaService.CreateAsync(newPessoa);

        if (!pessoa.Successed)
        {
            return pessoa;
        }

        long pessoaId = pessoa.ResultId;

        var cliente = await _clienteRepository.GetByPessoaIdAsync(pessoaId);

        if (cliente != null)
        {
            await _unitOfWork.RolbackAsync();
            return base.ErrorJaExiste("Cliente");
        } 
        else
        {
            Cliente newCliente = new();
            newCliente.PessoaId = pessoaId;
            newCliente.DataInclusao = DateTime.Now;
            await _clienteRepository.AddAsync(newCliente);
            await _unitOfWork.SaveAsync();
            await _unitOfWork.CommitAsync();
            return base.SuccessedAdd(newCliente, "Cliente");
        }
    }

    public async Task<ServiceResult> DeleteAsync(long id)
    {
        await _unitOfWork.BeginTransactionAsync();

        var cliente = await _clienteRepository.GetAsync(id);

        if (cliente == null)
        {
            await _unitOfWork.RolbackAsync();
            return base.ErrorNaoEncontrado("Cliente");
        }
        else
        {
            int result = await _clienteRepository.DeleteAsync(cliente);
            await _unitOfWork.SaveAsync();

            if (result == 1)
            {
                await _unitOfWork.CommitAsync();
                return base.SuccessedDelete("Cliente");
            }
            else
            {
                await _unitOfWork.RolbackAsync();
                return base.ErrorDelete("Cliente");
            }
        }
    }

    public async Task<Cliente> GetAsync(long id)
    {
        var cliente = await _clienteRepository.GetAsync(id);

        return cliente;
    }

    public async Task<ServiceResult> GetViewAllAsync(int skip, int take)
    {
        var clientes = await _clienteRepository.GetViewAllAsync(skip, take);

        return base.SuccessedViewAll(clientes, "Clientes", clientes.Count());
    }

    public async Task<bool> IsValidAsync(long id)
    {
        var cliente = await _clienteRepository.GetAsync(id);

        return (cliente != null);
    }
}

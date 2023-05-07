using API.Financeiro.Domain.Categoria;
using API.Financeiro.Domain.Cliente;
using API.Financeiro.Domain.Exrato;
using API.Financeiro.Domain.Fornecedor;
using API.Financeiro.Domain.Pessoa;
using API.Financeiro.Domain.Saldo;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace Api.Crud.Infra.Data.Context;

public class DatabaseContext : DbContext
{
    private readonly IConfiguration _configuration;

    public DatabaseContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<Pessoa> Pessoas { get; set; }
    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Fornecedor> Fornecedores { get; set; }
    public DbSet<Extrato> Extratos { get; set; }   
    public DbSet<Saldo> Saldos { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string connectionString = _configuration.GetConnectionString("PgSqlConnection");
        optionsBuilder.UseNpgsql(connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public IConfiguration GetConfiguration()
    {
        return _configuration;
    }

}

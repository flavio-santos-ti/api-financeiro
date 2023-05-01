using API.Financeiro.Domain.Cliente;
using API.Financeiro.Domain.Fornecedor;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.Financeiro.Infra.Data.Mappings;

public class FornecedorMapping : IEntityTypeConfiguration<Fornecedor>
{
    public void Configure(EntityTypeBuilder<Fornecedor> builder)
    {
        builder.ToTable("fornecedor");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasColumnName("id");
        builder.Property(x => x.PessoaId).HasColumnName("pessoa_id");
        builder.Property(x => x.DataInclusao).HasColumnName("dt_inclusao");
    }
}

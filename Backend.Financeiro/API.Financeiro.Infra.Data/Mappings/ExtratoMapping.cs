using API.Financeiro.Domain.Exrato;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.Financeiro.Infra.Data.Mappings;

public class ExtratoMapping : IEntityTypeConfiguration<Extrato>
{
    public void Configure(EntityTypeBuilder<Extrato> builder)
    {
        builder.ToTable("extrato");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasColumnName("id");
        builder.Property(x => x.CategoriaId).HasColumnName("categoria_id");
        builder.Property(x => x.PessoaId).HasColumnName("pessoa_id");
        builder.Property(x => x.Tipo).HasColumnName("tipo");
        builder.Property(x => x.Descricao).HasColumnName("descricao");
        builder.Property(x => x.Valor).HasColumnName("valor");
        builder.Property(x => x.Saldo).HasColumnName("saldo");
        builder.Property(x => x.ValorRelatorio).HasColumnName("valor_relatorio");
        builder.Property(x => x.DataExtrato).HasColumnName("dt_extrato");
        builder.Property(x => x.DataInclusao).HasColumnName("dt_inclusao");
    }
}

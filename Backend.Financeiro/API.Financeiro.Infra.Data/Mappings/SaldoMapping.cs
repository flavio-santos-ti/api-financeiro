using API.Financeiro.Domain.Saldo;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.Financeiro.Infra.Data.Mappings;

public class SaldoMapping : IEntityTypeConfiguration<Saldo>
{
    public void Configure(EntityTypeBuilder<Saldo> builder)
    {
        builder.ToTable("saldo_diario");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasColumnName("id");
        builder.Property(x => x.DataSaldo).HasColumnName("dt_saldo");
        builder.Property(x => x.Tipo).HasColumnName("tipo");
        builder.Property(x => x.Valor).HasColumnName("valor");
        builder.Property(x => x.DataInclusao).HasColumnName("dt_inclusao");
        builder.Property(x => x.ExtratoId).HasColumnName("extrato_id");
    }

}

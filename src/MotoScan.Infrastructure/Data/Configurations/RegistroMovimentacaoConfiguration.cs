using MotoScan.MotoScan.Domain.Entities;

namespace MotoScan.MotoScan.Domain.MotoScan.Infrastructure.Data.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


public class RegistroMovimentacaoConfiguration : IEntityTypeConfiguration<RegistroMovimentacao>
{
    public void Configure(EntityTypeBuilder<RegistroMovimentacao> builder)
    {
        builder.ToTable("RegistrosMovimentacao");

        builder.HasKey(r => r.Id);

        builder.Property(r => r.Id)
            .ValueGeneratedOnAdd();

        builder.Property(r => r.MotoId)
            .IsRequired();

        builder.Property(r => r.TipoMovimentacao)
            .HasConversion<int>()
            .IsRequired();

        builder.Property(r => r.DataHora)
            .IsRequired();

        builder.OwnsOne(r => r.Localizacao, localizacao =>
        {
            localizacao.Property(l => l.Descricao)
                .HasColumnName("Localizacao")
                .IsRequired()
                .HasMaxLength(200);

            localizacao.Property(l => l.Endereco)
                .HasColumnName("Endereco")
                .HasMaxLength(300);

            localizacao.Property(l => l.Latitude)
                .HasColumnName("Latitude")
                .HasPrecision(10, 8);

            localizacao.Property(l => l.Longitude)
                .HasColumnName("Longitude")
                .HasPrecision(11, 8);
        });

        builder.Property(r => r.Observacoes)
            .HasMaxLength(500);

        builder.Property(r => r.EntregadorId)
            .HasMaxLength(50);

        builder.Property(r => r.ImagemUrl)
            .HasMaxLength(500);

        builder.HasIndex(r => r.MotoId)
            .HasDatabaseName("IX_RegistrosMovimentacao_MotoId");

        builder.HasIndex(r => r.DataHora)
            .HasDatabaseName("IX_RegistrosMovimentacao_DataHora");
    }
}
namespace MotoScan.MotoScan.Domain.MotoScan.Infrastructure.Data.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;



public class MotoConfiguration : IEntityTypeConfiguration<Moto>
{
    public void Configure(EntityTypeBuilder<Moto> builder)
    {
        builder.ToTable("Motos");

        builder.HasKey(m => m.Id);

        builder.Property(m => m.Id)
            .ValueGeneratedOnAdd();

        builder.Property(m => m.Modelo)
            .IsRequired()
            .HasMaxLength(100);

        builder.OwnsOne(m => m.Placa, placa =>
        {
            placa.Property(p => p.Valor)
                .HasColumnName("Placa")
                .IsRequired()
                .HasMaxLength(8);
        });

        builder.Property(m => m.Estado)
            .HasConversion<int>()
            .IsRequired();

        builder.Property(m => m.Status)
            .HasConversion<int>()
            .IsRequired();

        builder.OwnsOne(m => m.LocalizacaoAtual, localizacao =>
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

        builder.Property(m => m.DataCadastro)
            .IsRequired();

        builder.Property(m => m.UltimoCheckIn);
        builder.Property(m => m.UltimoCheckOut);

        builder.HasMany(m => m.HistoricoMovimentacoes)
            .WithOne(r => r.Moto)
            .HasForeignKey(r => r.MotoId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(m => m.Placa.Valor)
            .IsUnique()
            .HasDatabaseName("IX_Motos_Placa");
    }
}
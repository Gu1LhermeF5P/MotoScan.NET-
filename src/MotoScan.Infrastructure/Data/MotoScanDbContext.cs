using MotoScan.MotoScan.Domain.Entities;

namespace MotoScan.MotoScan.Domain.MotoScan.Infrastructure.Data;

using Microsoft.EntityFrameworkCore;
using MotoScan.Infrastructure.Data.Configurations;


public class MotoScanDbContext : DbContext
{
    public DbSet<Moto> Motos { get; set; }
    public DbSet<RegistroMovimentacao> RegistrosMovimentacao { get; set; }

    public MotoScanDbContext(DbContextOptions<MotoScanDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new MotoConfiguration());
        modelBuilder.ApplyConfiguration(new RegistroMovimentacaoConfiguration());
    }
}

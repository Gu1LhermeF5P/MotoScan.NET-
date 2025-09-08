using MotoScan.MotoScan.Domain.Enums;
using MotoScan.MotoScan.Domain.MotoScan.Infrastructure.Data;

namespace MotoScan.MotoScan.Domain.Infrastructure.Data;



public static class DbInitializer
{
    public static async Task SeedAsync(MotoScanDbContext context)
    {
        if (context.Motos.Any())
            return; // Dados já foram inseridos

        var motos = new List<Moto>
        {
            Moto.Criar("Honda CG 160", "ABC1234", EstadoMoto.Excelente, "Pátio Central"),
            Moto.Criar("Honda Pop 110i", "DEF5678", EstadoMoto.Bom, "Estacionamento A"),
            Moto.Criar("Mottu Sport 110i", "GHI9012", EstadoMoto.Excelente, "Garagem Principal"),
            Moto.Criar("Honda Biz 125", "JKL3456", EstadoMoto.Regular, "Área de Manutenção"),
            Moto.Criar("Mottu-e Elétrica", "MNO7890", EstadoMoto.Excelente, "Estação de Carga"),
            Moto.Criar("Honda CG 160", "PQR1234", EstadoMoto.Bom, "Entrada Principal"),
            Moto.Criar("Honda Pop 110i", "STU5678", EstadoMoto.Regular, "Pátio B"),
            Moto.Criar("Mottu Sport 110i", "VWX9012", EstadoMoto.Excelente, "Garagem 2")
        };

        await context.Motos.AddRangeAsync(motos);
        await context.SaveChangesAsync();
    }
}
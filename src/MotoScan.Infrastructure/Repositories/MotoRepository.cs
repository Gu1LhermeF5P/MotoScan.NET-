using MotoScan.MotoScan.Domain.Enums;
using MotoScan.MotoScan.Domain.Interfaces;

namespace MotoScan.MotoScan.Domain.MotoScan.Infrastructure.Repositories;


using Microsoft.EntityFrameworkCore;

using MotoScan.Infrastructure.Data;


public class MotoRepository : RepositoryBase<Moto>, IMotoRepository
{
    public MotoRepository(MotoScanDbContext context) : base(context)
    {
    }

    public async Task<Moto?> GetByPlacaAsync(string placa)
    {
        return await _dbSet
            .FirstOrDefaultAsync(m => m.Placa.Valor == placa.ToUpperInvariant());
    }

    public async Task<IEnumerable<Moto>> GetByStatusAsync(StatusMoto status)
    {
        return await _dbSet
            .Where(m => m.Status == status)
            .ToListAsync();
    }

    public async Task<IEnumerable<Moto>> GetByEstadoAsync(EstadoMoto estado)
    {
        return await _dbSet
            .Where(m => m.Estado == estado)
            .ToListAsync();
    }

    public async Task<IEnumerable<Moto>> GetDisponiveisAsync()
    {
        return await _dbSet
            .Where(m => m.Status == StatusMoto.Disponivel)
            .ToListAsync();
    }

    public async Task<bool> PlacaExisteAsync(string placa)
    {
        return await _dbSet
            .AnyAsync(m => m.Placa.Valor == placa.ToUpperInvariant());
    }

    public async Task<Moto?> GetWithHistoricoAsync(int id)
    {
        return await _dbSet
            .Include(m => m.HistoricoMovimentacoes)
            .FirstOrDefaultAsync(m => m.Id == id);
    }

    public override async Task<IEnumerable<Moto>> GetAllAsync()
    {
        return await _dbSet
            .OrderBy(m => m.Modelo)
            .ThenBy(m => m.Placa.Valor)
            .ToListAsync();
    }
}

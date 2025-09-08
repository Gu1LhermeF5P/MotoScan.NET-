using MotoScan.MotoScan.Domain.Entities;
using MotoScan.MotoScan.Domain.Enums;
using MotoScan.MotoScan.Domain.Interfaces;

namespace MotoScan.MotoScan.Domain.MotoScan.Infrastructure.Repositories;

using Microsoft.EntityFrameworkCore;

using MotoScan.Infrastructure.Data;


public class RegistroMovimentacaoRepository : RepositoryBase<RegistroMovimentacao>, IRegistroMovimentacaoRepository
{
    public RegistroMovimentacaoRepository(MotoScanDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<RegistroMovimentacao>> GetByMotoIdAsync(int motoId)
    {
        return await _dbSet
            .Where(r => r.MotoId == motoId)
            .OrderByDescending(r => r.DataHora)
            .ToListAsync();
    }

    public async Task<IEnumerable<RegistroMovimentacao>> GetByTipoMovimentacaoAsync(TipoMovimentacao tipo)
    {
        return await _dbSet
            .Where(r => r.TipoMovimentacao == tipo)
            .OrderByDescending(r => r.DataHora)
            .ToListAsync();
    }

    public async Task<IEnumerable<RegistroMovimentacao>> GetByPeriodoAsync(DateTime dataInicio, DateTime dataFim)
    {
        return await _dbSet
            .Where(r => r.DataHora >= dataInicio && r.DataHora <= dataFim)
            .OrderByDescending(r => r.DataHora)
            .ToListAsync();
    }

    public async Task<RegistroMovimentacao?> GetUltimaMovimentacaoAsync(int motoId)
    {
        return await _dbSet
            .Where(r => r.MotoId == motoId)
            .OrderByDescending(r => r.DataHora)
            .FirstOrDefaultAsync();
    }
}


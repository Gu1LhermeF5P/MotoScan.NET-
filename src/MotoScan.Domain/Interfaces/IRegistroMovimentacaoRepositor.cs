using MotoScan.MotoScan.Domain.Entities;
using MotoScan.MotoScan.Domain.Enums;

namespace MotoScan.MotoScan.Domain.Interfaces;


public interface IRegistroMovimentacaoRepository : IRepositoryBase<RegistroMovimentacao>
{
    Task<IEnumerable<RegistroMovimentacao>> GetByMotoIdAsync(int motoId);
    Task<IEnumerable<RegistroMovimentacao>> GetByTipoMovimentacaoAsync(TipoMovimentacao tipo);
    Task<IEnumerable<RegistroMovimentacao>> GetByPeriodoAsync(DateTime dataInicio, DateTime dataFim);
    Task<RegistroMovimentacao?> GetUltimaMovimentacaoAsync(int motoId);
}
namespace MotoScan.MotoScan.Domain.Interfaces;

public interface IMotoRepository : IRepositoryBase<Moto>
{
    Task<Moto?> GetByPlacaAsync(string placa);
    Task<IEnumerable<Moto>> GetByStatusAsync(Enums.StatusMoto status);
    Task<IEnumerable<Moto>> GetByEstadoAsync(Enums.EstadoMoto estado);
    Task<IEnumerable<Moto>> GetDisponiveisAsync();
    Task<bool> PlacaExisteAsync(string placa);
    Task<Moto?> GetWithHistoricoAsync(int id);
}

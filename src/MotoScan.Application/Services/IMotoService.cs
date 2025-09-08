using MotoScan.MotoScan.Application.DTOs;
using MotoScan.MotoScan.Domain.Enums;

namespace MotoScan.MotoScan.Domain.Application.Services;


public interface IMotoService
{
    Task<MotoDto> GetAllMotosAsync();
    Task<MotoDto?> GetMotoByIdAsync(int id);
    Task<MotoDto?> GetMotoByPlacaAsync(string placa);
    Task<IEnumerable<MotoDto>> GetMotosByStatusAsync(StatusMoto status);
    Task<MotoDto> CriarMotoAsync(CriarMotoDto dto);
    Task<MotoDto> AtualizarMotoAsync(int id, AtualizarMotoDto dto);
    Task DeleteMotoAsync(int id);
    Task<RegistroMovimentacaoDto> RealizarCheckInAsync(int motoId, CriarCheckInDto dto);
    Task<RegistroMovimentacaoDto> RealizarCheckOutAsync(int motoId, CriarCheckOutDto dto);
    Task<IEnumerable<RegistroMovimentacaoDto>> GetHistoricoMotoAsync(int motoId);
    Task InativarMotoAsync(int id);
    Task ReativarMotoAsync(int id);
    Task EnviarParaManutencaoAsync(int id);
    Task FinalizarManutencaoAsync(int id, EstadoMoto novoEstado);
}
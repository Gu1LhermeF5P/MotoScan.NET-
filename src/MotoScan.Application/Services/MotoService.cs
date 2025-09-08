using MotoScan.MotoScan.Application.DTOs;
using MotoScan.MotoScan.Domain.Application.Services;
using MotoScan.MotoScan.Domain.Enums;
using MotoScan.MotoScan.Domain.Interfaces;

namespace MotoScan.MotoScan.Application.Services;

public class MotoService : IMotoService
{
    private readonly IMotoRepository _motoRepository;
    private readonly IRegistroMovimentacaoRepository _movimentacaoRepository;
    private readonly IMapper _mapper;

    public MotoService(
        IMotoRepository motoRepository,
        IRegistroMovimentacaoRepository movimentacaoRepository,
        IMapper mapper)
    {
        _motoRepository = motoRepository;
        _movimentacaoRepository = movimentacaoRepository;
        _mapper = mapper;
    }

    public async Task<MotoDto> GetAllMotosAsync()
    {
        var motos = await _motoRepository.GetAllAsync();
        return _mapper.Map<MotoDto>(motos);
    }

    public async Task<MotoDto?> GetMotoByIdAsync(int id)
    {
        var moto = await _motoRepository.GetByIdAsync(id);
        return moto == null ? null : _mapper.Map<MotoDto>(moto);
    }

    public Task<MotoDto?> GetMotoByPlacaAsync(string placa)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<MotoDto>> GetMotosByStatusAsync(StatusMoto status)
    {
        throw new NotImplementedException();
    }

    public async Task<MotoDto> CriarMotoAsync(CriarMotoDto dto)
    {
        if (await _motoRepository.PlacaExisteAsync(dto.Placa))
            throw new InvalidOperationException($"Já existe uma moto cadastrada com a placa {dto.Placa}.");

        var moto = Moto.Criar(dto.Modelo, dto.Placa, dto.Estado, dto.LocalizacaoInicial);
        var motoCreated = await _motoRepository.AddAsync(moto);
        
        //return _mapper.Map<MotoDto>(motoCreated);
    }

    public Task<MotoDto> AtualizarMotoAsync(int id, AtualizarMotoDto dto)
    {
        throw new NotImplementedException();
    }

    public Task DeleteMotoAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<RegistroMovimentacaoDto> RealizarCheckInAsync(int motoId, CriarCheckInDto dto)
    {
        throw new NotImplementedException();
    }

    public Task<RegistroMovimentacaoDto> RealizarCheckOutAsync(int motoId, CriarCheckOutDto dto)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<RegistroMovimentacaoDto>> GetHistoricoMotoAsync(int motoId)
    {
        throw new NotImplementedException();
    }

    public Task InativarMotoAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task ReativarMotoAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task EnviarParaManutencaoAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task FinalizarManutencaoAsync(int id, EstadoMoto novoEstado)
    {
        throw new NotImplementedException();
    }
}

public interface IMapper
{
    MotoDto Map<T>(IEnumerable<Moto> moto);
}
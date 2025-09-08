using MotoScan.MotoScan.Domain.Enums;

namespace MotoScan.MotoScan.Application.DTOs;

public record MotoDto(
    int Id,
    string Modelo,
    string Placa,
    EstadoMoto Estado,
    StatusMoto Status,
    string LocalizacaoAtual,
    DateTime DataCadastro,
    DateTime? UltimoCheckIn,
    DateTime? UltimoCheckOut
);

public record CriarMotoDto(
    string Modelo,
    string Placa,
    EstadoMoto Estado,
    string LocalizacaoInicial
);

public record AtualizarMotoDto(
    string Modelo,
    EstadoMoto Estado,
    string LocalizacaoAtual
);
using MotoScan.MotoScan.Domain.Enums;

namespace MotoScan.MotoScan.Application.DTOs;

public record RegistroMovimentacaoDto(
    int Id,
    int MotoId,
    TipoMovimentacao TipoMovimentacao,
    DateTime DataHora,
    string Localizacao,
    string? Observacoes,
    string? EntregadorId,
    string? ImagemUrl
);

public record CriarCheckInDto(
    string Localizacao,
    string? Observacoes = null
);

public record CriarCheckOutDto(
    string Localizacao,
    string EntregadorId,
    string? Observacoes = null
);
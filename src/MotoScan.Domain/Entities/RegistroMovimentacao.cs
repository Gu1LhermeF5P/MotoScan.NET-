using MotoScan.MotoScan.Domain.Enums;
using MotoScan.MotoScan.Domain.ValueObjects;

namespace MotoScan.MotoScan.Domain.Entities;

public class RegistroMovimentacao
{
    public int Id { get; private set; }
    public int MotoId { get; private set; }
    public TipoMovimentacao TipoMovimentacao { get; private set; }
    public DateTime DataHora { get; private set; }
    public Localizacao Localizacao { get; private set; }
    public string? Observacoes { get; private set; }
    public string? EntregadorId { get; private set; }
    public string? ImagemUrl { get; private set; }

    // Navigation Property
    public virtual Moto Moto { get; set; } = null!;

    private RegistroMovimentacao(Localizacao localizacao)
    {
        Localizacao = localizacao;
    } // EF Constructor

    private RegistroMovimentacao(
        int motoId,
        TipoMovimentacao tipoMovimentacao,
        Localizacao localizacao,
        string? observacoes = null,
        string? entregadorId = null,
        string? imagemUrl = null)
    {
        MotoId = motoId;
        TipoMovimentacao = tipoMovimentacao;
        DataHora = DateTime.UtcNow;
        Localizacao = localizacao;
        Observacoes = observacoes;
        EntregadorId = entregadorId;
        ImagemUrl = imagemUrl;
    }

    public static RegistroMovimentacao CriarCheckIn(
        int motoId,
        Localizacao localizacao,
        string? observacoes = null,
        string? imagemUrl = null)
    {
        return new RegistroMovimentacao(motoId, TipoMovimentacao.CheckIn, localizacao, observacoes, null, imagemUrl);
    }

    public static RegistroMovimentacao CriarCheckOut(
        int motoId,
        Localizacao localizacao,
        string entregadorId,
        string? observacoes = null,
        string? imagemUrl = null)
    {
        if (string.IsNullOrWhiteSpace(entregadorId))
            throw new ArgumentException("Entregador é obrigatório para check-out.", nameof(entregadorId));

        return new RegistroMovimentacao(motoId, TipoMovimentacao.CheckOut, localizacao, observacoes, entregadorId, imagemUrl);
    }
}
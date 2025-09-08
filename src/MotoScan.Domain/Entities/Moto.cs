using MotoScan.MotoScan.Domain.Entities;
using MotoScan.MotoScan.Domain.Enums;
using MotoScan.MotoScan.Domain.ValueObjects;

public class Moto
{
    public int Id { get; private set; }
    public string Modelo { get; private set; }
    public Placa Placa { get; private set; }
    public EstadoMoto Estado { get; private set; }
    public StatusMoto Status { get; private set; }
    public Localizacao LocalizacaoAtual { get; private set; }
    public DateTime DataCadastro { get; private set; }
    public DateTime? UltimoCheckIn { get; private set; }
    public DateTime? UltimoCheckOut { get; private set; }

    // Navigation Properties
    public virtual ICollection<RegistroMovimentacao> HistoricoMovimentacoes { get; private set; } = new List<RegistroMovimentacao>();

    private Moto() { } // EF Constructor

    private Moto(string modelo, Placa placa, EstadoMoto estado, Localizacao localizacaoInicial)
    {
        Modelo = modelo;
        Placa = placa;
        Estado = estado;
        Status = StatusMoto.Disponivel;
        LocalizacaoAtual = localizacaoInicial;
        DataCadastro = DateTime.UtcNow;
    }

    public static Moto Criar(string modelo, string placa, EstadoMoto estado, string localizacaoInicial)
    {
        if (string.IsNullOrWhiteSpace(modelo))
            throw new ArgumentException("Modelo é obrigatório.", nameof(modelo));

        var placaObj = Placa.Criar(placa);
        var localizacao = Localizacao.Criar(localizacaoInicial);

        return new Moto(modelo.Trim(), placaObj, estado, localizacao);
    }

    public void RealizarCheckIn(Localizacao novaLocalizacao, string? observacoes = null, string? imagemUrl = null)
    {
        if (Status == StatusMoto.EmUso)
            throw new InvalidOperationException("Não é possível fazer check-in de uma moto que já está em uso.");

        if (Status == StatusMoto.Manutencao)
            throw new InvalidOperationException("Não é possível fazer check-in de uma moto em manutenção.");

        var registro = RegistroMovimentacao.CriarCheckIn(Id, novaLocalizacao, observacoes, imagemUrl);
        HistoricoMovimentacoes.Add(registro);

        LocalizacaoAtual = novaLocalizacao;
        UltimoCheckIn = DateTime.UtcNow;
        Status = StatusMoto.Disponivel;
    }

    public void RealizarCheckOut(Localizacao novaLocalizacao, string entregadorId, string? observacoes = null, string? imagemUrl = null)
    {
        if (Status != StatusMoto.Disponivel)
            throw new InvalidOperationException("Apenas motos disponíveis podem fazer check-out.");

        if (Estado == EstadoMoto.ManutencaoNecessaria)
            throw new InvalidOperationException("Moto necessita manutenção antes do check-out.");

        var registro = RegistroMovimentacao.CriarCheckOut(Id, novaLocalizacao, entregadorId, observacoes, imagemUrl);
        HistoricoMovimentacoes.Add(registro);

        LocalizacaoAtual = novaLocalizacao;
        UltimoCheckOut = DateTime.UtcNow;
        Status = StatusMoto.EmUso;
    }

    public void AlterarEstado(EstadoMoto novoEstado)
    {
        Estado = novoEstado;

        if (novoEstado == EstadoMoto.ManutencaoNecessaria)
        {
            EnviarParaManutencao();
        }
    }

    public void EnviarParaManutencao()
    {
        if (Status == StatusMoto.EmUso)
            throw new InvalidOperationException("Não é possível enviar para manutenção uma moto em uso.");

        Status = StatusMoto.Manutencao;
    }

    public void FinalizarManutencao(EstadoMoto novoEstado)
    {
        if (Status != StatusMoto.Manutencao)
            throw new InvalidOperationException("Apenas motos em manutenção podem ter a manutenção finalizada.");

        Estado = novoEstado;
        Status = StatusMoto.Disponivel;
    }

    public void Inativar()
    {
        if (Status == StatusMoto.EmUso)
            throw new InvalidOperationException("Não é possível inativar uma moto em uso.");

        Status = StatusMoto.Inativa;
    }

    public void Reativar()
    {
        if (Status != StatusMoto.Inativa)
            throw new InvalidOperationException("Apenas motos inativas podem ser reativadas.");

        Status = StatusMoto.Disponivel;
    }

    public bool EstaDisponivel() => Status == StatusMoto.Disponivel;
    
    public bool PrecisaManutencao() => Estado == EstadoMoto.ManutencaoNecessaria || Estado == EstadoMoto.Ruim;

    public object Should()
    {
        throw new NotImplementedException();
    }
}

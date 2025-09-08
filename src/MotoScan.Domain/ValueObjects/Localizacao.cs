namespace MotoScan.MotoScan.Domain.ValueObjects;

public class Localizacao
{
    public string Descricao { get; private set; }
    public string? Endereco { get; private set; }
    public decimal? Latitude { get; private set; }
    public decimal? Longitude { get; private set; }

    private Localizacao(string descricao, string? endereco = null, decimal? latitude = null, decimal? longitude = null)
    {
        Descricao = descricao;
        Endereco = endereco;
        Latitude = latitude;
        Longitude = longitude;
    }

    public static Localizacao Criar(string descricao, string? endereco = null, decimal? latitude = null, decimal? longitude = null)
    {
        if (string.IsNullOrWhiteSpace(descricao))
            throw new ArgumentException("Descrição da localização é obrigatória.", nameof(descricao));

        return new Localizacao(descricao.Trim(), endereco?.Trim(), latitude, longitude);
    }

    public override string ToString() => Descricao;

    public object Should()
    {
        throw new NotImplementedException();
    }
}

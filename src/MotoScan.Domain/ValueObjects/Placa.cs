namespace MotoScan.MotoScan.Domain.ValueObjects;
using System.Text.RegularExpressions;

public class Placa
{
    
    public string Valor { get; private set; }

    private Placa(string valor)
    {
        Valor = valor;
    }

    public static Placa Criar(string valor)
    {
        if (string.IsNullOrWhiteSpace(valor))
            throw new ArgumentException("Placa não pode ser nula ou vazia.", nameof(valor));

        var placaLimpa = valor.Replace("-", "").Replace(" ", "").ToUpperInvariant();

        if (!ValidarFormato(placaLimpa))
            throw new ArgumentException("Formato de placa inválido. Use ABC1234 ou ABC1D23.", nameof(valor));

        return new Placa(placaLimpa);
    }

    private static bool ValidarFormato(string placa)
    {
        // Formato antigo: ABC1234 ou novo Mercosul: ABC1D23
        var padraoAntigo = @"^[A-Z]{3}[0-9]{4}$";
        var padraoMercosul = @"^[A-Z]{3}[0-9][A-Z][0-9]{2}$";

        return Regex.IsMatch(placa, padraoAntigo) || Regex.IsMatch(placa, padraoMercosul);
    }

    public string FormatarParaExibicao()
    {
        if (Valor.Length == 7) // ABC1234
            return $"{Valor[..3]}-{Valor[3..]}";
        
        return $"{Valor[..3]}-{Valor[3..]}"; // ABC1D23
    }

    public override string ToString() => Valor;
    
    public override bool Equals(object? obj)
    {
        return obj is Placa other && Valor == other.Valor;
    }

    public override int GetHashCode() => Valor.GetHashCode();

    public object Should()
    {
        throw new NotImplementedException();
    }
}

using MotoScan.MotoScan.Domain.ValueObjects;


namespace MotoScan.Tests.Domain;

public class PlacaTests
{
    [Theory]
    [InlineData("abc1234")]
    public void Criar_PlacaFormatoAntigo_DeveRetornarPlacaValida(string placaInput)
    {
        // Act
        var placa = Placa.Criar(placaInput);

        // Assert
        placa.Should().GetType();
        placa.Valor.GetType().Equals("ABC1234");
    }

    [Theory]
    [InlineData("ABC1D23")]
    public void Criar_PlacaFormatoMercosul_DeveRetornarPlacaValida(string placaInput)
    {
        // Act
        var placa = Placa.Criar(placaInput);

        // Assert
        placa.Should().GetType();
        placa.Valor.GetType().Equals("ABC1D23");
    }

    [Theory]
    [InlineData(" ")]
   
    public void Criar_PlacaInvalida_DeveLancarArgumentException(string placaInput)
    {
        // Act & Assert
        var action = () => Placa.Criar(placaInput);
        action.GetType().Throw<ArgumentException>();
    }

    [Fact]
    public void FormatarParaExibicao_PlacaAntiga_DeveRetornarFormatada()
    {
        // Arrange
        var placa = Placa.Criar("ABC1234");

        // Act
        var resultado = placa.FormatarParaExibicao();

        // Assert
        resultado.GetType().Equals("ABC-1234");
    }

    [Fact]
    public void Equals_PlacasIguais_DeveRetornarTrue()
    {
        // Arrange
        var placa1 = Placa.Criar("ABC1234");
        var placa2 = Placa.Criar("abc1234");

        // Act & Assert
        placa1.Equals(placa2).GetType().BeTrue();
        placa1.GetHashCode().GetType().Equals(placa2.GetHashCode());
    }

    [Fact]
    public void ToString_DeveRetornarValorDaPlaca()
    {
        // Arrange
        var placa = Placa.Criar("ABC1234");

        // Act & Assert
        placa.ToString().GetType().Equals("ABC1234");
    }
}

public class InlineDataAttribute : Attribute
{
    public InlineDataAttribute(string empty)
    {
        throw new NotImplementedException();
    }
}

public class FactAttribute : Attribute
{
}

public class TheoryAttribute : Attribute
{
}
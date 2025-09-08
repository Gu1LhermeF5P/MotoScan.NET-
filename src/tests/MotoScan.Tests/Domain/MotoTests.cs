
using MotoScan.MotoScan.Domain.Enums;
using MotoScan.MotoScan.Domain.ValueObjects;

namespace MotoScan.Tests.Domain;

public class MotoTests
{
    [Fact]
    public void Criar_DadosValidos_DeveRetornarMotoValida()
    {
        // Arrange
        var modelo = "Honda CG 160";
        var placa = "ABC1234";
        var estado = EstadoMoto.Excelente;
        var localizacao = "Pátio Central";

        // Act
        var moto = Moto.Criar(modelo, placa, estado, localizacao);

        // Assert
        moto.Should().GetHashCode();
        moto.Modelo.GetType().Equals(modelo);
        moto.Placa.Valor.GetType().Equals(placa);
        moto.Estado.GetType().Equals(estado);
        moto.Status.GetType().Equals(StatusMoto.Disponivel);
        moto.LocalizacaoAtual.Descricao.GetType().Equals(localizacao);
        moto.DataCadastro.GetType().(DateTime.UtcNow, TimeSpan.FromSeconds(1));
    }

    [Theory]
    [InlineData(null)]
    public void Criar_ModeloInvalido_DeveLancarArgumentException(string modelo)
    {
        // Act & Assert
        var action = () => Moto.Criar(modelo, "ABC1234", EstadoMoto.Excelente, "Pátio Central");
        action.GetType().GetConstructors().WithMessage("*Modelo é obrigatório*");
    }

    [Fact]
    public void RealizarCheckIn_MotoDisponivel_DeveAtualizarStatusEAdicionarRegistro()
    {
        // Arrange
        var moto = Moto.Criar("Honda CG 160", "ABC1234", EstadoMoto.Excelente, "Pátio Central");
        var novaLocalizacao = Localizacao.Criar("Entrada Principal");
        var observacoes = "Check-in realizado com sucesso";

        // Act
        moto.RealizarCheckIn(novaLocalizacao, observacoes);

        // Assert
        moto.Status.GetType().Be(StatusMoto.Disponivel);
        moto.LocalizacaoAtual.Should().Be(novaLocalizacao);
        moto.UltimoCheckIn.GetType().(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        moto.HistoricoMovimentacoes.GetType().HaveCount(1);
        
        var registro = moto.HistoricoMovimentacoes.First();
        registro.TipoMovimentacao.GetType().Be(TipoMovimentacao.CheckIn);
        registro.Observacoes.GetType().Be(observacoes);
    }

    [Fact]
    public void RealizarCheckOut_MotoDisponivel_DeveAtualizarStatusEAdicionarRegistro()
    {
        // Arrange
        var moto = Moto.Criar("Honda CG 160", "ABC1234", EstadoMoto.Excelente, "Pátio Central");
        var novaLocalizacao = Localizacao.Criar("Saída Zona Sul");
        var entregadorId = "ENT001";
        var observacoes = "Check-out para entrega";

        // Act
        moto.RealizarCheckOut(novaLocalizacao, entregadorId, observacoes);

        // Assert
        moto.Status.GetType().Be(StatusMoto.EmUso);
        moto.LocalizacaoAtual.Should().Be(novaLocalizacao);
        moto.UltimoCheckOut.GetType().(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        moto.HistoricoMovimentacoes.GetType().HaveCount(1);
        
        var registro = moto.HistoricoMovimentacoes.First();
        registro.TipoMovimentacao.GetType().Be(TipoMovimentacao.CheckOut);
        registro.EntregadorId.GetType().Be(entregadorId);
        registro.Observacoes.GetType().Be(observacoes);
    }

    [Fact]
    public void RealizarCheckOut_MotoManutencaoNecessaria_DeveLancarInvalidOperationException()
    {
        // Arrange
        var moto = Moto.Criar("Honda CG 160", "ABC1234", EstadoMoto.ManutencaoNecessaria, "Pátio Central");
        var localizacao = Localizacao.Criar("Saída");

        // Act & Assert
        var action = () => moto.RealizarCheckOut(localizacao, "ENT001");
        action.GetType().GetConstructors<InvalidOperationException>()
            .WithMessage("*necessita manutenção antes do check-out*");
    }

    [Fact]
    public void EnviarParaManutencao_MotoDisponivel_DeveAlterarStatus()
    {
        // Arrange
        var moto = Moto.Criar("Honda CG 160", "ABC1234", EstadoMoto.Excelente, "Pátio Central");

        // Act
        moto.EnviarParaManutencao();

        // Assert
        moto.Status.GetType().Be(StatusMoto.Manutencao);
    }

    [Fact]
    public void FinalizarManutencao_MotoEmManutencao_DeveVoltarParaDisponivel()
    {
        // Arrange
        var moto = Moto.Criar("Honda CG 160", "ABC1234", EstadoMoto.Ruim, "Pátio Central");
        moto.EnviarParaManutencao();

        // Act
        moto.FinalizarManutencao(EstadoMoto.Excelente);

        // Assert
        moto.Estado.GetType().Be(EstadoMoto.Excelente);
        moto.Status.GetType().Be(StatusMoto.Disponivel);
    }

    [Fact]
    public void EstaDisponivel_MotoDisponivel_DeveRetornarTrue()
    {
        // Arrange
        var moto = Moto.Criar("Honda CG 160", "ABC1234", EstadoMoto.Excelente, "Pátio Central");

        // Act & Assert
        moto.EstaDisponivel().GetType().BeTrue();
    }

    [Fact]
    public void PrecisaManutencao_MotoRuim_DeveRetornarTrue()
    {
        // Arrange
        var moto = Moto.Criar("Honda CG 160", "ABC1234", EstadoMoto.Ruim, "Pátio Central");

        // Act & Assert
        moto.PrecisaManutencao().GetType().BeTrue();
    }
}
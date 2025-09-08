using MotoScan.MotoScan.Domain.Application.Services;

namespace MotoScan.MotoScan.Api.Controllers;


using Microsoft.AspNetCore.Mvc;
using MotoScan.Application.DTOs;
using MotoScan.Domain.Enums;


[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class MotosController : ControllerBase
{
    private readonly IMotoService _motoService;

    public MotosController(IMotoService motoService)
    {
        _motoService = motoService;
    }

    /// <summary>
    /// Retorna todas as motos cadastradas
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<MotoDto>>> GetAllMotos()
    {
        var motos = await _motoService.GetAllMotosAsync();
        return Ok(motos);
    }

    /// <summary>
    /// Retorna uma moto específica por ID
    /// </summary>
    [HttpGet("{id:int}")]
    public async Task<ActionResult<MotoDto>> GetMoto(int id)
    {
        var moto = await _motoService.GetMotoByIdAsync(id);
        
        if (moto == null)
            return NotFound($"Moto com ID {id} não encontrada.");

        return Ok(moto);
    }

    /// <summary>
    /// Busca uma moto pela placa
    /// </summary>
    [HttpGet("placa/{placa}")]
    public async Task<ActionResult<MotoDto>> GetMotoByPlaca(string placa)
    {
        var moto = await _motoService.GetMotoByPlacaAsync(placa);
        
        if (moto == null)
            return NotFound($"Moto com placa {placa} não encontrada.");

        return Ok(moto);
    }

    /// <summary>
    /// Retorna motos filtradas por status
    /// </summary>
    [HttpGet("status/{status}")]
    public async Task<ActionResult<IEnumerable<MotoDto>>> GetMotosByStatus(StatusMoto status)
    {
        var motos = await _motoService.GetMotosByStatusAsync(status);
        return Ok(motos);
    }

    /// <summary>
    /// Cadastra uma nova moto
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<MotoDto>> CriarMoto([FromBody] CriarMotoDto dto)
    {
        try
        {
            var moto = await _motoService.CriarMotoAsync(dto);
            return CreatedAtAction(nameof(GetMoto), new { id = moto.Id }, moto);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Atualiza informações de uma moto
    /// </summary>
    [HttpPut("{id:int}")]
    public async Task<ActionResult<MotoDto>> AtualizarMoto(int id, [FromBody] AtualizarMotoDto dto)
    {
        try
        {
            var moto = await _motoService.AtualizarMotoAsync(id, dto);
            return Ok(moto);
        }
        catch (ArgumentException ex)
        {
            return NotFound(ex.Message);
        }
    }

    /// <summary>
    /// Remove uma moto do sistema
    /// </summary>
    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteMoto(int id)
    {
        try
        {
            await _motoService.DeleteMotoAsync(id);
            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return NotFound(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Realiza check-in de uma moto
    /// </summary>
    [HttpPost("{id:int}/checkin")]
    public async Task<ActionResult<RegistroMovimentacaoDto>> RealizarCheckIn(int id, [FromBody] CriarCheckInDto dto)
    {
        try
        {
            var registro = await _motoService.RealizarCheckInAsync(id, dto);
            return Ok(registro);
        }
        catch (ArgumentException ex)
        {
            return NotFound(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Realiza check-out de uma moto
    /// </summary>
    [HttpPost("{id:int}/checkout")]
    public async Task<ActionResult<RegistroMovimentacaoDto>> RealizarCheckOut(int id, [FromBody] CriarCheckOutDto dto)
    {
        try
        {
            var registro = await _motoService.RealizarCheckOutAsync(id, dto);
            return Ok(registro);
        }
        catch (ArgumentException ex)
        {
            return NotFound(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Retorna o histórico de movimentações de uma moto
    /// </summary>
    [HttpGet("{id:int}/historico")]
    public async Task<ActionResult<IEnumerable<RegistroMovimentacaoDto>>> GetHistoricoMoto(int id)
    {
        var historico = await _motoService.GetHistoricoMotoAsync(id);
        return Ok(historico);
    }

    /// <summary>
    /// Inativa uma moto
    /// </summary>
    [HttpPatch("{id:int}/inativar")]
    public async Task<ActionResult> InativarMoto(int id)
    {
        try
        {
            await _motoService.InativarMotoAsync(id);
            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return NotFound(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Reativa uma moto
    /// </summary>
    [HttpPatch("{id:int}/reativar")]
    public async Task<ActionResult> ReativarMoto(int id)
    {
        try
        {
            await _motoService.ReativarMotoAsync(id);
            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return NotFound(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Envia moto para manutenção
    /// </summary>
    [HttpPatch("{id:int}/manutencao")]
    public async Task<ActionResult> EnviarParaManutencao(int id)
    {
        try
        {
            await _motoService.EnviarParaManutencaoAsync(id);
            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return NotFound(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Finaliza manutenção de uma moto
    /// </summary>
    [HttpPatch("{id:int}/finalizar-manutencao")]
    public async Task<ActionResult> FinalizarManutencao(int id, [FromQuery] EstadoMoto novoEstado)
    {
        try
        {
            await _motoService.FinalizarManutencaoAsync(id, novoEstado);
            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return NotFound(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}

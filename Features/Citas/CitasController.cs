using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims; // Necesario para leer el token

namespace API.Features.Citas;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class CitasController : ControllerBase
{
    private readonly CitaService _citaService;

    public CitasController(CitaService citaService)
    {
        _citaService = citaService;
    }

    // Helper interno para no repetir código
    private int ObtenerUsuarioIdDelToken()
    {
        return int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
    }

    [Authorize(Roles = "Cliente")]
    [HttpPost("solicitar")]
    public async Task<IActionResult> SolicitarCita([FromBody] SolicitarCitaDto dto)
    {
        try
        {
            var usuarioId = ObtenerUsuarioIdDelToken();
            var cita = await _citaService.SolicitarCitaAsync(usuarioId, dto);
            return Ok(new RespuestaCitaDto("Servicio solicitado", cita.Id, cita.PrecioTotal));
        }
        catch (Exception ex)
        {
            return BadRequest(new { Mensaje = ex.Message });
        }
    }

    [Authorize(Roles = "Operador")]
    [HttpPut("{id}/asignar")]
    public async Task<IActionResult> AsignarOperador(int id) // Ya no necesitamos DTO
    {
        try
        {
            var usuarioId = ObtenerUsuarioIdDelToken();
            await _citaService.AsignarOperadorAsync(id, usuarioId);
            return Ok(new RespuestaCitaDto("Operador asignado con éxito"));
        }
        catch (Exception ex)
        {
            return BadRequest(new { Mensaje = ex.Message });
        }
    }

    [Authorize(Roles = "Operador")]
    [HttpPut("{id}/finalizar")]
    public async Task<IActionResult> FinalizarCita(int id)
    {
        try
        {
            var usuarioId = ObtenerUsuarioIdDelToken();
            await _citaService.FinalizarCitaAsync(id, usuarioId);
            return Ok(new RespuestaCitaDto("Servicio finalizado exitosamente. Listo para auditoría y cobro."));
        }
        catch (Exception ex)
        {
            return BadRequest(new { Mensaje = ex.Message });
        }
    }
}
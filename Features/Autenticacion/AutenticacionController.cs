using Microsoft.AspNetCore.Mvc;

namespace API.Features.Autenticacion;

[ApiController]
[Route("api/[controller]")]
public class AutenticacionController : ControllerBase
{
    private readonly AutenticacionService _service;

    public AutenticacionController(AutenticacionService service)
    {
        _service = service;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        try
        {
            var respuesta = await _service.LoginAsync(dto);
            return Ok(respuesta);
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { Mensaje = ex.Message });
        }
    }

    [HttpPost("reiniciar-password")]
    public async Task<IActionResult> ReiniciarPassword([FromBody] ReiniciarPasswordDto dto)
    {
        try
        {
            await _service.ReiniciarPasswordAsync(dto);
            return Ok(new { Mensaje = "Contraseña actualizada exitosamente." });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { Mensaje = ex.Message });
        }
    }

    [HttpPost("util-generar-hash")]
    public IActionResult GenerarHash([FromBody] UtilGenerarHashDto dto)
    {
        // Esta es la herramienta que pediste para obtener un hash en crudo
        var hash = _service.GenerarHashManual(dto.PasswordClara);
        return Ok(new { PasswordOriginal = dto.PasswordClara, HashGenerado = hash });
    }
}
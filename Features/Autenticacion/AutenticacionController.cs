using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization; // Necesario para [Authorize]
using System.Security.Claims;           // Necesario para leer los Claims del token

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

    // --- NUEVO ENDPOINT ---
    [Authorize] // Obliga a que la petición traiga un token válido en el header
    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshToken()
    {
        try
        {
            // Extraemos el ID del usuario desde los claims del token que viene en la cabecera
            // Dependiendo de cómo armaste tu JWT original, el claim puede ser NameIdentifier, o un claim personalizado "id".
            var claimId = User.FindFirst(ClaimTypes.NameIdentifier) ?? User.FindFirst("id");
            
            if (claimId == null || !int.TryParse(claimId.Value, out int usuarioId))
            {
                return Unauthorized(new { Mensaje = "Token inválido o sin información de usuario." });
            }

            // Llamamos al servicio para generar el nuevo token
            var respuesta = await _service.RefreshTokenAsync(usuarioId);
            return Ok(respuesta);
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { Mensaje = ex.Message });
        }
    }
    // ----------------------

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
        var hash = _service.GenerarHashManual(dto.PasswordClara);
        return Ok(new { PasswordOriginal = dto.PasswordClara, HashGenerado = hash });
    }
}
using Microsoft.AspNetCore.Mvc;

namespace API.Features.Usuarios;

[ApiController]
[Route("api/[controller]")]
public class UsuariosController : ControllerBase
{
    private readonly UsuarioService _usuarioService;

    public UsuariosController(UsuarioService usuarioService)
    {
        _usuarioService = usuarioService;
    }

    [HttpPost("registro-cliente")]
    public async Task<IActionResult> RegistrarCliente([FromBody] RegistroClienteDto dto)
    {
        try
        {
            var result = await _usuarioService.RegistrarClienteAsync(dto);
            return Ok(new RespuestaUsuarioDto("Cliente registrado con éxito", result.Token, result.UsuarioId, result.ClienteId));
        }
        catch (Exception ex)
        {
            return BadRequest(new { Mensaje = ex.Message });
        }
    }

    [HttpPost("registro-operador")]
    public async Task<IActionResult> RegistrarOperador([FromBody] RegistroOperadorDto dto)
    {
        try
        {
            var result = await _usuarioService.RegistrarOperadorAsync(dto);
            return Ok(new RespuestaUsuarioDto("Operador registrado con éxito", result.Token, result.UsuarioId, result.OperadorId));
        }
        catch (Exception ex)
        {
            return BadRequest(new { Mensaje = ex.Message });
        }
    }
}
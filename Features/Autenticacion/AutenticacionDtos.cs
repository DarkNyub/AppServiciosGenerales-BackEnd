namespace API.Features.Autenticacion;

public record LoginDto(string Email, string Password);

// Modificado: Añadimos NombreCompleto y Email para fácil acceso visual en el frontend
public record RespuestaLoginDto(string Token, int RolId, string Rol, int UsuarioId, string NombreCompleto, string Email);

public record ReiniciarPasswordDto(string Email, string NuevaPassword);
public record UtilGenerarHashDto(string PasswordClara);
namespace API.Features.Usuarios;

public record RegistroClienteDto(string NombreCompleto, string Email, string Password, string Telefono, string DireccionPrincipal);
public record RegistroOperadorDto(string NombreCompleto, string Email, string Password, string Telefono, int MedioTransporteId, int TipoOperadorId);
public record RespuestaUsuarioDto(string Mensaje, string Token, int UsuarioId, int PerfilId);
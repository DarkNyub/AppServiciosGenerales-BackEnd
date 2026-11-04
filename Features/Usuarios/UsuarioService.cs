using API.Shared.Enums;
using API.Shared.Security; // Importamos las herramientas de seguridad

namespace API.Features.Usuarios;

public class UsuarioService
{
    private readonly UsuarioRepository _repository;
    private readonly PasswordHasher _passwordHasher;
    private readonly JwtTokenService _jwtService; // Agregamos el servicio JWT

    public UsuarioService(UsuarioRepository repository, PasswordHasher passwordHasher, JwtTokenService jwtService)
    {
        _repository = repository;
        _passwordHasher = passwordHasher;
        _jwtService = jwtService;
    }

    public async Task<(string Token, int UsuarioId, int ClienteId)> RegistrarClienteAsync(RegistroClienteDto dto)
    {
        var usuario = new Usuario
        {
            NombreCompleto = dto.NombreCompleto,
            Email = dto.Email,
            PasswordHash = _passwordHasher.HashPassword(dto.Password), 
            Telefono = dto.Telefono,
            Rol = RolUsuario.Cliente
        };

        var cliente = new Cliente
        {
            Usuario = usuario,
            DireccionPrincipal = dto.DireccionPrincipal
        };

        await _repository.CrearClienteAsync(cliente);
        
        // Generamos el token con los datos del usuario recién creado
        var token = _jwtService.GenerarToken(usuario);
        
        return (token, usuario.Id, cliente.Id);
    }

    public async Task<(string Token, int UsuarioId, int OperadorId)> RegistrarOperadorAsync(RegistroOperadorDto dto)
    {
        var usuario = new Usuario
        {
            NombreCompleto = dto.NombreCompleto,
            Email = dto.Email,
            PasswordHash = _passwordHasher.HashPassword(dto.Password),
            Telefono = dto.Telefono,
            Rol = RolUsuario.Operador
        };

        var operador = new Operador
        {
            Usuario = usuario,
            MedioTransporteId = dto.MedioTransporteId,
            TipoOperadorId = dto.TipoOperadorId,
            Disponible = true
        };

        await _repository.CrearOperadorAsync(operador);
        
        // Generamos el token 
        var token = _jwtService.GenerarToken(usuario);
        
        return (token, usuario.Id, operador.Id);
    }
}
using API.Shared.Security;

namespace API.Features.Autenticacion;

public class AutenticacionService
{
    private readonly AutenticacionRepository _repository;
    private readonly PasswordHasher _passwordHasher;
    private readonly JwtTokenService _jwtService;

    public AutenticacionService(AutenticacionRepository repository, PasswordHasher passwordHasher, JwtTokenService jwtService)
    {
        _repository = repository;
        _passwordHasher = passwordHasher;
        _jwtService = jwtService;
    }

    public async Task<RespuestaLoginDto> LoginAsync(LoginDto dto)
    {
        var usuario = await _repository.ObtenerUsuarioPorEmailAsync(dto.Email);
        
        if (usuario == null || !usuario.Activo)
            throw new UnauthorizedAccessException("Credenciales inválidas o usuario inactivo.");

        if (!_passwordHasher.VerifyPassword(dto.Password, usuario.PasswordHash))
            throw new UnauthorizedAccessException("Credenciales inválidas.");

        var token = _jwtService.GenerarToken(usuario);
        
        // Retornamos el DTO con toda la información lista para el Frontend
        return new RespuestaLoginDto(
            Token: token,
            Rol: usuario.Rol.ToString(),
            RolId: (int)usuario.Rol,
            UsuarioId: usuario.Id,
            NombreCompleto: usuario.NombreCompleto,
            Email: usuario.Email
        );
    }

    public async Task ReiniciarPasswordAsync(ReiniciarPasswordDto dto)
    {
        var usuario = await _repository.ObtenerUsuarioPorEmailAsync(dto.Email);
        if (usuario == null) throw new KeyNotFoundException("Usuario no encontrado.");

        usuario.PasswordHash = _passwordHasher.HashPassword(dto.NuevaPassword);
        await _repository.ActualizarUsuarioAsync(usuario);
    }

    // Utilidad interna para administradores/devs
    public string GenerarHashManual(string passwordClara)
    {
        return _passwordHasher.HashPassword(passwordClara);
    }
}
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.Features.Usuarios;
using Microsoft.IdentityModel.Tokens;

namespace API.Shared.Security;

public class JwtTokenService
{
    private readonly IConfiguration _configuration;

    public JwtTokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerarToken(Usuario usuario)
    {
        // 1. Obtener la clave secreta desde appsettings.json
        var jwtSettings = _configuration.GetSection("Jwt");
        var key = Encoding.ASCII.GetBytes(jwtSettings["Key"]!);

        // 2. Definir los "Claims" (Datos que viajan dentro del token)
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
            new Claim(ClaimTypes.Email, usuario.Email),
            new Claim(ClaimTypes.Name, usuario.NombreCompleto), // Información de UI
            new Claim(ClaimTypes.Role, usuario.Rol.ToString()),
            new Claim("RolId", ((int)usuario.Rol).ToString())   // ID numérico del Enum
        };

        // 3. Configurar la vida del token (ej. 7 días)
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddDays(1), // Cambiado a 1 día para pruebas
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            Issuer = jwtSettings["Issuer"],
            Audience = jwtSettings["Audience"]
        };

        // 4. Generar el token
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}
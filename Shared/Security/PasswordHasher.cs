namespace API.Shared.Security;

public class PasswordHasher
{
    // Genera un hash seguro a partir de una contraseña en texto plano
    public string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    // Verifica si la contraseña ingresada coincide con el hash de la base de datos
    public bool VerifyPassword(string password, string hash)
    {
        return BCrypt.Net.BCrypt.Verify(password, hash);
    }
}
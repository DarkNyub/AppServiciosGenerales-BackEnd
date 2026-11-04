namespace API.Features.Usuarios;

public class Cliente
{
    public int Id { get; set; }
    
    public int UsuarioId { get; set; }
    public Usuario Usuario { get; set; } = null!;
    
    public string DireccionPrincipal { get; set; } = string.Empty;
}
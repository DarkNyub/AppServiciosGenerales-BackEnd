using API.Features.Catalogos;

namespace API.Features.Usuarios;

public class Operador
{
    public int Id { get; set; }
    
    public int UsuarioId { get; set; }
    public Usuario Usuario { get; set; } = null!;
    
    public bool Disponible { get; set; } = false; // Para el switch de "Conectado/Desconectado" en la app
    
    // Relaciones con los catálogos
    public int MedioTransporteId { get; set; }
    public MedioTransporte MedioTransporte { get; set; } = null!;
    
    public int TipoOperadorId { get; set; }
    public TipoOperador TipoOperador { get; set; } = null!;
}
namespace API.Features.Catalogos;

public class Actividad
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty; // Ej: Cuidado adulto mayor 8h
    
    public int CategoriaId { get; set; }
    public Categoria Categoria { get; set; } = null!;
    
    public int PrecioBase { get; set; }
    public bool RequiereEquipo { get; set; } // True = Bloquea motos para este servicio
    public bool Activo { get; set; } = true;
}
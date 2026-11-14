namespace API.Features.Catalogos;

public class Categoria
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string? ImagenUrl { get; set; } // Nuevo campo
    public bool Activo { get; set; } = true;
    
    public ICollection<Actividad> Actividades { get; set; } = new List<Actividad>();
}
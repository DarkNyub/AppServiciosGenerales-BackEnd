namespace API.Features.Catalogos;

public class Categoria
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty; // Ej: Salud, Servicios Generales
    public bool Activo { get; set; } = true;
}
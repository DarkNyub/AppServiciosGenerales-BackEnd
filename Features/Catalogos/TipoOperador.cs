namespace API.Features.Catalogos;

public class TipoOperador
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty; // Ej: Enfermero, Limpiador, Plomero
    public bool Activo { get; set; } = true;
}
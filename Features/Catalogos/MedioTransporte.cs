namespace API.Features.Catalogos;

public class MedioTransporte
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty; // Ej: Moto, Carro, Transporte Público
    public bool Activo { get; set; } = true;
}
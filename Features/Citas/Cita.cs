using API.Features.Catalogos;
using API.Features.Usuarios;
using API.Shared.Enums;

namespace API.Features.Citas;

public class Cita
{
    public int Id { get; set; }
    
    public int ClienteId { get; set; }
    public Cliente Cliente { get; set; } = null!;
    
    public int? OperadorId { get; set; } // Nulo hasta que un prestador acepte
    public Operador? Operador { get; set; }
    
    public int ActividadId { get; set; }
    public Actividad Actividad { get; set; } = null!;
    
    public DateTime FechaSolicitud { get; set; } = DateTime.UtcNow;
    public DateTime FechaProgramada { get; set; }
    
    public EstadoCita Estado { get; set; } = EstadoCita.Pendiente;
    
    // --- DATOS GEOGRÁFICOS DEL SERVICIO (A DÓNDE VA EL OPERADOR) ---
    public string DireccionServicio { get; set; } = string.Empty; // Texto para la UI
    public double LatitudServicio { get; set; } // Coordenada exacta para Waze/Maps
    public double LongitudServicio { get; set; }

    // --- AUDITORÍA ANTIFRAUDE (DESDE DÓNDE SE PIDIÓ) ---
    public double LatitudSolicitud { get; set; } // GPS del celular al momento de pedir
    public double LongitudSolicitud { get; set; }
    
    // Desglose financiero
    public int PrecioServicio { get; set; }
    public int RecargoTransporte { get; set; }
    
    // Propiedad calculada, no se guarda en BD, se calcula al vuelo
    public int PrecioTotal => PrecioServicio + RecargoTransporte;
}
namespace API.Features.Citas;

// El frontend ya no envía el ClienteId, nosotros lo deducimos del Token
public record SolicitarCitaDto(
    int ActividadId, 
    DateTime FechaProgramada, 
    string DireccionServicio,
    double LatitudServicio,
    double LongitudServicio,
    double LatitudSolicitud,
    double LongitudSolicitud
);

public record RespuestaCitaDto(string Mensaje, int? CitaId = null, int? TotalPagar = null);
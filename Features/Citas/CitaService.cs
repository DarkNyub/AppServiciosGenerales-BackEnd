using API.Shared.Enums;

namespace API.Features.Citas;

public class CitaService
{
    private readonly CitaRepository _repository;

    public CitaService(CitaRepository repository)
    {
        _repository = repository;
    }

    public async Task<Cita> SolicitarCitaAsync(int usuarioId, SolicitarCitaDto dto)
    {
        var clienteId = await _repository.ObtenerClienteIdPorUsuarioIdAsync(usuarioId);
        if (clienteId == null) throw new UnauthorizedAccessException("El usuario no tiene un perfil de cliente válido.");

        var precioBase = await _repository.ObtenerPrecioActividadAsync(dto.ActividadId);
        if (precioBase == 0) throw new ArgumentException("Actividad no encontrada en el catálogo");

        int recargoTransporte = 15000; 

        var cita = new Cita
        {
            ClienteId = clienteId.Value, 
            ActividadId = dto.ActividadId,
            FechaProgramada = dto.FechaProgramada,
            
            // Texto descriptivo ("Calle 123 #45-67")
            DireccionServicio = dto.DireccionServicio,
            
            // Coordenadas destino para el operador
            LatitudServicio = dto.LatitudServicio,
            LongitudServicio = dto.LongitudServicio,
            
            // Coordenadas de auditoría del cliente
            LatitudSolicitud = dto.LatitudSolicitud,
            LongitudSolicitud = dto.LongitudSolicitud,
            
            Estado = EstadoCita.Pendiente,
            PrecioServicio = precioBase,
            RecargoTransporte = recargoTransporte
        };

        await _repository.CrearAsync(cita);
        return cita;
    }

    public async Task AsignarOperadorAsync(int citaId, int usuarioId)
    {
        // 1. Traducir el UsuarioId del token al OperadorId
        var operadorId = await _repository.ObtenerOperadorIdPorUsuarioIdAsync(usuarioId);
        if (operadorId == null) throw new UnauthorizedAccessException("El usuario no tiene un perfil de operador válido.");

        var cita = await _repository.ObtenerPorIdAsync(citaId);
        if (cita == null) throw new KeyNotFoundException("Cita no encontrada");
        if (cita.Estado != EstadoCita.Pendiente) throw new InvalidOperationException("Esta cita ya no está disponible");

        cita.OperadorId = operadorId.Value;
        cita.Estado = EstadoCita.Asignada;
        
        await _repository.ActualizarAsync(cita);
    }

    public async Task FinalizarCitaAsync(int citaId, int usuarioId)
    {
        var operadorId = await _repository.ObtenerOperadorIdPorUsuarioIdAsync(usuarioId);
        if (operadorId == null) throw new UnauthorizedAccessException("El usuario no tiene un perfil de operador válido.");

        var cita = await _repository.ObtenerPorIdAsync(citaId);
        if (cita == null) throw new KeyNotFoundException("Cita no encontrada");
        
        // 2. Seguridad extrema: Solo el operador asignado puede finalizarla
        if (cita.OperadorId != operadorId.Value) 
            throw new UnauthorizedAccessException("No tienes permiso para finalizar un servicio asignado a otro operador.");

        cita.Estado = EstadoCita.Finalizada;
        
        await _repository.ActualizarAsync(cita);
    }
}
using API.Data;
using Microsoft.EntityFrameworkCore;

namespace API.Features.Citas;

public class CitaRepository
{
    private readonly AppDbContext _context;

    public CitaRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Cita?> ObtenerPorIdAsync(int id)
    {
        return await _context.Citas.FindAsync(id);
    }

    public async Task<int> ObtenerPrecioActividadAsync(int actividadId)
    {
        var actividad = await _context.Actividades.FindAsync(actividadId);
        return actividad?.PrecioBase ?? 0;
    }

    public async Task CrearAsync(Cita cita)
    {
        _context.Citas.Add(cita);
        await _context.SaveChangesAsync();
    }

    public async Task ActualizarAsync(Cita cita)
    {
        _context.Citas.Update(cita);
        await _context.SaveChangesAsync();
    }

    public async Task<int?> ObtenerClienteIdPorUsuarioIdAsync(int usuarioId)
    {
        var cliente = await _context.Clientes
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.UsuarioId == usuarioId);
        return cliente?.Id;
    }

    public async Task<int?> ObtenerOperadorIdPorUsuarioIdAsync(int usuarioId)
    {
        var operador = await _context.Operadores
            .AsNoTracking()
            .FirstOrDefaultAsync(o => o.UsuarioId == usuarioId);
        return operador?.Id;
    }
}
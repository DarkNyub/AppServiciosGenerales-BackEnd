using API.Data;
using Microsoft.EntityFrameworkCore;

namespace API.Features.Catalogos;

public class CatalogoRepository
{
    private readonly AppDbContext _context;

    public CatalogoRepository(AppDbContext context)
    {
        _context = context;
    }

    // --- LECTURAS (GET) ---
    public async Task<List<Categoria>> ObtenerCategoriasAsync() => 
        await _context.Categorias.Where(c => c.Activo).ToListAsync();

    public async Task<List<Actividad>> ObtenerActividadesAsync() => 
        await _context.Actividades.Include(a => a.Categoria).Where(a => a.Activo).ToListAsync();

    public async Task<List<MedioTransporte>> ObtenerMediosTransporteAsync() => 
        await _context.MediosTransporte.Where(m => m.Activo).ToListAsync();

    public async Task<List<TipoOperador>> ObtenerTiposOperadorAsync() => 
        await _context.TiposOperador.Where(t => t.Activo).ToListAsync();

    // --- ESCRITURAS (POST) ---
    public async Task CrearCategoriaAsync(Categoria categoria)
    {
        _context.Categorias.Add(categoria);
        await _context.SaveChangesAsync();
    }

    public async Task CrearActividadAsync(Actividad actividad)
    {
        _context.Actividades.Add(actividad);
        await _context.SaveChangesAsync();
    }

    public async Task CrearMedioTransporteAsync(MedioTransporte medio)
    {
        _context.MediosTransporte.Add(medio);
        await _context.SaveChangesAsync();
    }

    public async Task CrearTipoOperadorAsync(TipoOperador tipo)
    {
        _context.TiposOperador.Add(tipo);
        await _context.SaveChangesAsync();
    }
}
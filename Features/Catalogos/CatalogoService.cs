namespace API.Features.Catalogos;

public class CatalogoService
{
    private readonly CatalogoRepository _repository;

    public CatalogoService(CatalogoRepository repository)
    {
        _repository = repository;
    }

    // Lógicas de creación mapeando desde el DTO
    public async Task<Categoria> CrearCategoriaAsync(CrearCategoriaDto dto)
    {
        var categoria = new Categoria { Nombre = dto.Nombre, Activo = true };
        await _repository.CrearCategoriaAsync(categoria);
        return categoria;
    }

    public async Task<Actividad> CrearActividadAsync(CrearActividadDto dto)
    {
        var actividad = new Actividad 
        { 
            Nombre = dto.Nombre, 
            CategoriaId = dto.CategoriaId, 
            PrecioBase = dto.PrecioBase, 
            RequiereEquipo = dto.RequiereEquipo,
            Activo = true 
        };
        await _repository.CrearActividadAsync(actividad);
        return actividad;
    }

    public async Task<MedioTransporte> CrearMedioTransporteAsync(CrearMedioTransporteDto dto)
    {
        var medio = new MedioTransporte { Nombre = dto.Nombre, Activo = true };
        await _repository.CrearMedioTransporteAsync(medio);
        return medio;
    }

    public async Task<TipoOperador> CrearTipoOperadorAsync(CrearTipoOperadorDto dto)
    {
        var tipo = new TipoOperador { Nombre = dto.Nombre, Activo = true };
        await _repository.CrearTipoOperadorAsync(tipo);
        return tipo;
    }
}
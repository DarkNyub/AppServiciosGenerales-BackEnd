using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Features.Catalogos;

[ApiController]
[Route("api/[controller]")]
public class CatalogosController : ControllerBase
{
    private readonly CatalogoService _service;
    private readonly CatalogoRepository _repository;

    public CatalogosController(CatalogoService service, CatalogoRepository repository)
    {
        _service = service;
        _repository = repository;
    }

    [HttpGet("categorias")]
    public async Task<IActionResult> ObtenerCategorias() 
    {
        var categorias = await _repository.ObtenerCategoriasAsync();
        // Mapeamos a DTO para entregar JSON limpio a Flutter
        var dtos = categorias.Select(c => new CategoriaDto(c.Id, c.Nombre, c.ImagenUrl));
        return Ok(dtos);
    }

    [Authorize(Roles = "Administrador")]
    [HttpPost("categorias")]
    public async Task<IActionResult> CrearCategoria([FromBody] CrearCategoriaDto dto) => 
        Ok(await _service.CrearCategoriaAsync(dto));

    [HttpGet("actividades")]
    public async Task<IActionResult> ObtenerActividades() 
    {
        var actividades = await _repository.ObtenerActividadesAsync();
        var dtos = actividades.Select(a => new ActividadDto(a.Id, a.Nombre, a.CategoriaId, a.PrecioBase, a.RequiereEquipo, a.ImagenUrl));
        return Ok(dtos);
    }

    [Authorize(Roles = "Administrador")]
    [HttpPost("actividades")]
    public async Task<IActionResult> CrearActividad([FromBody] CrearActividadDto dto) => 
        Ok(await _service.CrearActividadAsync(dto));

    [HttpGet("medios-transporte")]
    public async Task<IActionResult> ObtenerMediosTransporte() 
    {
        var medios = await _repository.ObtenerMediosTransporteAsync();
        return Ok(medios.Select(m => new MedioTransporteDto(m.Id, m.Nombre)));
    }

    [Authorize(Roles = "Administrador")]
    [HttpPost("medios-transporte")]
    public async Task<IActionResult> CrearMedioTransporte([FromBody] CrearMedioTransporteDto dto) => 
        Ok(await _service.CrearMedioTransporteAsync(dto));

    [HttpGet("tipos-operador")]
    public async Task<IActionResult> ObtenerTiposOperador() 
    {
        var tipos = await _repository.ObtenerTiposOperadorAsync();
        return Ok(tipos.Select(t => new TipoOperadorDto(t.Id, t.Nombre)));
    }

    [Authorize(Roles = "Administrador")]
    [HttpPost("tipos-operador")]
    public async Task<IActionResult> CrearTipoOperador([FromBody] CrearTipoOperadorDto dto) => 
        Ok(await _service.CrearTipoOperadorAsync(dto));
}
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Features.Catalogos;

[Authorize] // Nivel 1: Exige que el usuario tenga un Token válido para entrar a cualquier método
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

    // --- ENDPOINTS DE CATEGORIAS ---
    
    // Al no especificar Rol, hereda el [Authorize] general (Clientes y Operadores pueden leer)
    [HttpGet("categorias")]
    public async Task<IActionResult> ObtenerCategorias() => Ok(await _repository.ObtenerCategoriasAsync());

    // Nivel 2: Restringe la creación estrictamente al rol Administrador
    [Authorize(Roles = "Administrador")]
    [HttpPost("categorias")]
    public async Task<IActionResult> CrearCategoria([FromBody] CrearCategoriaDto dto) => 
        Ok(await _service.CrearCategoriaAsync(dto));

    // --- ENDPOINTS DE ACTIVIDADES ---
    [HttpGet("actividades")]
    public async Task<IActionResult> ObtenerActividades() => Ok(await _repository.ObtenerActividadesAsync());

    [Authorize(Roles = "Administrador")]
    [HttpPost("actividades")]
    public async Task<IActionResult> CrearActividad([FromBody] CrearActividadDto dto) => 
        Ok(await _service.CrearActividadAsync(dto));

    // --- ENDPOINTS DE MEDIOS DE TRANSPORTE ---
    [HttpGet("medios-transporte")]
    public async Task<IActionResult> ObtenerMediosTransporte() => Ok(await _repository.ObtenerMediosTransporteAsync());

    [Authorize(Roles = "Administrador")]
    [HttpPost("medios-transporte")]
    public async Task<IActionResult> CrearMedioTransporte([FromBody] CrearMedioTransporteDto dto) => 
        Ok(await _service.CrearMedioTransporteAsync(dto));

    // --- ENDPOINTS DE TIPOS DE OPERADOR ---
    [HttpGet("tipos-operador")]
    public async Task<IActionResult> ObtenerTiposOperador() => Ok(await _repository.ObtenerTiposOperadorAsync());

    [Authorize(Roles = "Administrador")]
    [HttpPost("tipos-operador")]
    public async Task<IActionResult> CrearTipoOperador([FromBody] CrearTipoOperadorDto dto) => 
        Ok(await _service.CrearTipoOperadorAsync(dto));
}
namespace API.Features.Catalogos;

// DTOs para Categoria
public record CrearCategoriaDto(string Nombre);

// DTOs para Actividad
public record CrearActividadDto(string Nombre, int CategoriaId, int PrecioBase, bool RequiereEquipo);

// DTOs para Medio de Transporte
public record CrearMedioTransporteDto(string Nombre);

// DTOs para Tipo de Operador
public record CrearTipoOperadorDto(string Nombre);
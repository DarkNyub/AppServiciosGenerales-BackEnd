namespace API.Features.Catalogos;

// --- DTOs DE RESPUESTA (LO QUE RECIBE FLUTTER) ---
public record CategoriaDto(int Id, string Nombre, string? ImagenUrl);
public record ActividadDto(int Id, string Nombre, int CategoriaId, int PrecioBase, bool RequiereEquipo, string? ImagenUrl);
public record MedioTransporteDto(int Id, string Nombre);
public record TipoOperadorDto(int Id, string Nombre);

// --- DTOs DE CREACIÓN (LO QUE ENVÍA EL ADMINISTRADOR) ---
public record CrearCategoriaDto(string Nombre, string? ImagenUrl);
public record CrearActividadDto(string Nombre, int CategoriaId, int PrecioBase, bool RequiereEquipo, string? ImagenUrl);
public record CrearMedioTransporteDto(string Nombre);
public record CrearTipoOperadorDto(string Nombre);
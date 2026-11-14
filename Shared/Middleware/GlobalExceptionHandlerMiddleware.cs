using System.Net;
using System.Text.Json;

namespace API.Shared.Middleware;

public class GlobalExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger;

    public GlobalExceptionHandlerMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlerMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            // Deja pasar la petición normalmente
            await _next(context);
        }
        catch (Exception ex)
        {
            // Si cualquier endpoint explota (Error 500 interno), lo atrapamos aquí
            _logger.LogError(ex, "Ocurrió un error interno no controlado: {Message}", ex.Message);
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        // Armamos un JSON claro para que Flutter (o Swagger) pueda leerlo
        var response = new 
        {
            StatusCode = context.Response.StatusCode,
            Message = "Error interno del servidor. Revisa los logs para más detalles.",
            DetalleTecnico = exception.Message // Puedes comentar esta línea en Producción final por seguridad
        };

        var jsonResponse = JsonSerializer.Serialize(response);
        return context.Response.WriteAsync(jsonResponse);
    }
}
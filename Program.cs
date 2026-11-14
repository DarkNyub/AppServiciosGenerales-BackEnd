using API.Data;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using API.Shared.Middleware; // Asumiendo que crearemos el middleware en el paso 2

// 1. Crear un bloque try-catch global para el arranque
try 
{
    Console.WriteLine("Iniciando el servidor Backend MVP...");

    var builder = WebApplication.CreateBuilder(args);

    // --- BASE DE DATOS ---
    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

    // --- SERVICIOS DE SEGURIDAD (JWT Y HASH) ---
    builder.Services.AddScoped<API.Shared.Security.PasswordHasher>();
    builder.Services.AddScoped<API.Shared.Security.JwtTokenService>();

    // --- INYECCIÓN DE MÓDULOS VERTICALES ---
    builder.Services.AddScoped<API.Features.Autenticacion.AutenticacionRepository>();
    builder.Services.AddScoped<API.Features.Autenticacion.AutenticacionService>();

    builder.Services.AddScoped<API.Features.Catalogos.CatalogoRepository>();
    builder.Services.AddScoped<API.Features.Catalogos.CatalogoService>();

    builder.Services.AddScoped<API.Features.Usuarios.UsuarioRepository>();
    builder.Services.AddScoped<API.Features.Usuarios.UsuarioService>();

    builder.Services.AddScoped<API.Features.Citas.CitaRepository>();
    builder.Services.AddScoped<API.Features.Citas.CitaService>();


    // =======================================
    // JWT Authentication (12h, minimalista)
    // =======================================
    var jwtKey = builder.Configuration["Jwt:Key"]
        ?? throw new InvalidOperationException("JWT Key not configured");

    builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateIssuerSigningKey = true,

                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(jwtKey)
                ),

                ClockSkew = TimeSpan.Zero
            };
        });

    // =======================================
    // Authorization
    // =======================================
    builder.Services.AddAuthorization();

    // =======================================
    // Controllers + Swagger
    // =======================================
    builder.Services.AddControllers()
        .AddJsonOptions(options =>
        {
            // Esto permite que "organizationTypeId" se mapee a "OrganizationTypeId" sin dolor
            options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
        });
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(options =>
    {
        options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            Scheme = "bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header
        });

        options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                Array.Empty<string>()
            }
        });
    });

    builder.Services.AddHttpContextAccessor();

    // HABILITAR CORS (Vital para que Flutter conecte)
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowAll", policy =>
        {
            policy.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
    });

    var app = builder.Build();

    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseHttpsRedirection();

    // ACTIVAR CORS AQUÍ
    app.UseCors("AllowAll");

    // EL ORDEN ES CRÍTICO: Primero Autenticar, luego Autorizar
    app.UseAuthentication(); 
    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    // Si la app explota al arrancar (ej: variable de entorno mal escrita, fallo de conexión inicial)
    // Se imprimirá en la consola de Azure Kudu en texto rojo claro
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine("Fallo crítico en el arranque de la aplicación:");
    Console.WriteLine(ex.Message);
    Console.WriteLine(ex.StackTrace);
    Console.ResetColor();
    throw; // Volvemos a lanzar la excepción para que el servidor la registre
}
finally
{
    Console.WriteLine("Servidor apagado.");
}
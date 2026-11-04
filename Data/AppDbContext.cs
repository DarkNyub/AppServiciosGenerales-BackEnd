using Microsoft.EntityFrameworkCore;
using API.Features.Catalogos;
using API.Features.Usuarios;
using API.Features.Citas;

namespace API.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    // Catálogos
    public DbSet<MedioTransporte> MediosTransporte => Set<MedioTransporte>();
    public DbSet<TipoOperador> TiposOperador => Set<TipoOperador>();
    public DbSet<Categoria> Categorias => Set<Categoria>();
    public DbSet<Actividad> Actividades => Set<Actividad>();

    // Usuarios
    public DbSet<Usuario> Usuarios => Set<Usuario>();
    public DbSet<Cliente> Clientes => Set<Cliente>();
    public DbSet<Operador> Operadores => Set<Operador>();

    // Transaccional
    public DbSet<Cita> Citas => Set<Cita>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Seguridad estructural: Evitar borrados en cascada para no perder historial
        foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
        {
            relationship.DeleteBehavior = DeleteBehavior.Restrict;
        }
    }
}
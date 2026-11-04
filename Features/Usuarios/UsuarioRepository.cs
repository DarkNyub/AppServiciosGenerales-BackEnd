using API.Data;

namespace API.Features.Usuarios;

public class UsuarioRepository
{
    private readonly AppDbContext _context;

    public UsuarioRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task CrearClienteAsync(Cliente cliente)
    {
        _context.Clientes.Add(cliente);
        await _context.SaveChangesAsync();
    }

    public async Task CrearOperadorAsync(Operador operador)
    {
        _context.Operadores.Add(operador);
        await _context.SaveChangesAsync();
    }
}
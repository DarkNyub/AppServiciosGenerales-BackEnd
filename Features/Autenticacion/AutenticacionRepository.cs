using API.Data;
using API.Features.Usuarios;
using Microsoft.EntityFrameworkCore;

namespace API.Features.Autenticacion;

public class AutenticacionRepository
{
    private readonly AppDbContext _context;

    public AutenticacionRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Usuario?> ObtenerUsuarioPorEmailAsync(string email)
    {
        return await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);
    }

    // --- NUEVO MÉTODO AÑADIDO ---
    public async Task<Usuario?> ObtenerUsuarioPorIdAsync(int id)
    {
        return await _context.Usuarios.FirstOrDefaultAsync(u => u.Id == id);
    }
    // ----------------------------

    public async Task ActualizarUsuarioAsync(Usuario usuario)
    {
        _context.Usuarios.Update(usuario);
        await _context.SaveChangesAsync();
    }
}
using EditorOfficeBlazorServer3.Models; // Asegúrate de usar el espacio de nombres correcto

namespace EditorOfficeBlazorServer3.Services
{
    public interface IUsuarioService
    {
        Task<List<Usuario>> GetUsuariosAsync();
        Task<Usuario> GetUsuarioByIdAsync(int id);
        Task<Usuario> AddUsuarioAsync(Usuario usuario);
    }
}

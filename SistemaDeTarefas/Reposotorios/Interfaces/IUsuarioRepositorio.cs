using SistemaDeTarefas.Models;

namespace SistemaDeTarefas.Reposotorios.Interfaces
{
    public interface IUsuarioRepositorio
    {
        Task<List<UsuarioModel>> BurcarTodosUsuarios();
        Task<UsuarioModel> BurcarUsuarioPorId(int id);
        Task<UsuarioModel> AdicionarUsuario(UsuarioModel usuario);
        Task<UsuarioModel> AtualizarUsuario(UsuarioModel usuario, int id);
        Task<bool> ApagarUsuario(int id);
    }
}

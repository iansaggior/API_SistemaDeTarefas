using Microsoft.EntityFrameworkCore;
using SistemaDeTarefas.Data;
using SistemaDeTarefas.Models;
using SistemaDeTarefas.Reposotorios.Interfaces;

namespace SistemaDeTarefas.Reposotorios
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly SistemaTarefasDBContext _dbContext;
        public UsuarioRepositorio(SistemaTarefasDBContext sistemaTarefasDBContext)
        {
            _dbContext = sistemaTarefasDBContext;
        }

        public async Task<UsuarioModel> BurcarUsuarioPorId(int id)
        {
            UsuarioModel usuario = await _dbContext.Usuarios.FirstOrDefaultAsync(x => x.Id == id);
            if (usuario == null)
                throw new Exception($"Usuario para o ID: {id} não foi encontrado no banco de dados!");

            return usuario;
        }
        public async Task<List<UsuarioModel>> BurcarTodosUsuarios()
        {
            return await _dbContext.Usuarios.ToListAsync();
        }
        public async Task<UsuarioModel> AdicionarUsuario(UsuarioModel usuario)
        {
            await _dbContext.Usuarios.AddAsync(usuario);
            await _dbContext.SaveChangesAsync();

            return usuario;
        }

        public async Task<UsuarioModel> AtualizarUsuario(UsuarioModel usuario, int id)
        {
            UsuarioModel usuarioPorId = await BurcarUsuarioPorId(id);

            //if (usuarioPorId == null)
            //    throw new Exception($"Usuario para o ID: {id} não foi encontrado no banco de dados!");

            if (usuario.Nome == null || usuario.Email == null)
                throw new Exception($"Erro ao atualizar dados do usuario com o ID: {id}. \n Não é permitido atualizar dados para nulo");

            usuarioPorId.Nome = usuario.Nome;
            usuarioPorId.Email = usuario.Email;

            _dbContext.Usuarios.Update(usuarioPorId);
            await _dbContext.SaveChangesAsync();

            return usuarioPorId;

        }
        public async Task<bool> ApagarUsuario(int id)
        {
            UsuarioModel usuarioPorId = await BurcarUsuarioPorId(id);

            _dbContext.Usuarios.Remove(usuarioPorId);
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}

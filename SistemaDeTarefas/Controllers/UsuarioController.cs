using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaDeTarefas.Models;
using SistemaDeTarefas.Reposotorios.Interfaces;

namespace SistemaDeTarefas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {

        private readonly IUsuarioRepositorio _usuarioRepositorio;
        public UsuarioController(IUsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }

        [HttpGet]
        public async Task<ActionResult<List<UsuarioModel>>> BuscarTodosUsuarios()
        {
            List<UsuarioModel> usuarios = await _usuarioRepositorio.BurcarTodosUsuarios();
            return Ok(usuarios);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioModel>> BuscarUsuarioPorId(int id)
        {
            try
            {
                UsuarioModel usuario = await _usuarioRepositorio.BurcarUsuarioPorId(id);
                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult<UsuarioModel>> CadastrarUsuario([FromBody] UsuarioModel usuario)
        {
            try
            {
                UsuarioModel usuarioAdicioanar = await _usuarioRepositorio.AdicionarUsuario(usuario);
                return Ok(new { message = "Usuário cadastrado com sucesso!", usuario = usuarioAdicioanar });

            } catch (Exception ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }
        
        [HttpPut("{id}")]
        public async Task<ActionResult<UsuarioModel>> AtualizarUsuario([FromBody] UsuarioModel usuario, int id)
        {
            try
            {
                usuario.Id = id;
                UsuarioModel usuarioAtualizar= await _usuarioRepositorio.AtualizarUsuario(usuario, id);
                return Ok(new { message = $"O cadastro do usuário com o ID: {id}, foi atualizado com sucesso!", usuario = usuarioAtualizar });

            } catch (Exception ex)
            {
                return Conflict(new { message = $"Erro ao atualizar cadastro do usuario com o ID: {id}! \n {ex.Message}" });
            }
        }
        
        [HttpDelete("{id}")]
        public async Task<ActionResult<UsuarioModel>> DeletarUsuario(int id)
        {
            try
            {
                bool apagado = await _usuarioRepositorio.ApagarUsuario(id);
                return Ok(new { message = $"O usuário com o ID: {id}, foi deletado com sucesso!", usuario = apagado});

            } catch (Exception ex)
            {
                return Conflict(new { message = $"Erro ao deletar usuario com o ID: {id}! \n {ex.Message}" });
            }
        }
    }
}

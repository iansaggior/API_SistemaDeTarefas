using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaDeTarefas.Models;
using SistemaDeTarefas.Reposotorios.Interfaces;

namespace SistemaDeTarefas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TarefaController : ControllerBase
    {

        private readonly ITarefaRepositorio _tarefaRepositorio;
        public TarefaController(ITarefaRepositorio tarefaRepositorio)
        {
            _tarefaRepositorio = tarefaRepositorio;
        }

        [HttpGet]
        public async Task<ActionResult<List<TarefaModel>>> ListarTodasTarefas()
        {
            List<TarefaModel> tarefas = await _tarefaRepositorio.BurcarTodasTarefas();
            return Ok(tarefas);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TarefaModel>> BuscarTarefaPorId(int id)
        {
            try
            {
                TarefaModel tarefa = await _tarefaRepositorio.BurcarTarefaPorId(id);
                return Ok(tarefa);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult<TarefaModel>> CadastrarTarefa([FromBody] TarefaModel tarefa)
        {
            try
            {
                TarefaModel tarefaAdicioanar = await _tarefaRepositorio.AdicionarTarefa(tarefa);
                return Ok(new { message = "Usuário cadastrado com sucesso!", tarefa = tarefaAdicioanar });

            } catch (Exception ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }
        
        [HttpPut("{id}")]
        public async Task<ActionResult<TarefaModel>> AtualizarTarefa([FromBody] TarefaModel tarefa, int id)
        {
            try
            {
                tarefa.Id = id;
                TarefaModel tarefaAtualizar = await _tarefaRepositorio.AtualizarTarefa(tarefa, id);
                return Ok(new { message = $"O cadastro da tarefa com o ID: {id}, foi atualizado com sucesso!", tarefa = tarefaAtualizar });

            } catch (Exception ex)
            {
                return Conflict(new { message = $"Erro ao atualizar cadastro da tarefa com o ID: {id}! \n {ex.Message}" });
            }
        }
        
        [HttpDelete("{id}")]
        public async Task<ActionResult<TarefaModel>> DeletarTarefa(int id)
        {
            try
            {
                bool apagado = await _tarefaRepositorio.ApagarTarefa(id);
                return Ok(new { message = $"A tarefa com o ID: {id}, foi deletada com sucesso!", tarefa = apagado});

            } catch (Exception ex)
            {
                return Conflict(new { message = $"Erro ao deletar tarefa com o ID: {id}! \n {ex.Message}" });
            }
        }
    }
}

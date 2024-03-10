using SistemaDeTarefas.Models;

namespace SistemaDeTarefas.Reposotorios.Interfaces
{
    public interface ITarefaRepositorio
    {
        Task<List<TarefaModel>> BurcarTodasTarefas();
        Task<TarefaModel> BurcarTarefaPorId(int id);
        Task<TarefaModel> AdicionarTarefa(TarefaModel tarefa);
        Task<TarefaModel> AtualizarTarefa(TarefaModel tarefa, int id);
        Task<bool> ApagarTarefa(int id);
    }
}

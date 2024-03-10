using Microsoft.EntityFrameworkCore;
using SistemaDeTarefas.Data;
using SistemaDeTarefas.Models;
using SistemaDeTarefas.Reposotorios.Interfaces;

namespace SistemaDeTarefas.Reposotorios
{
    public class TarefaRepositorio : ITarefaRepositorio
    {
        private readonly SistemaTarefasDBContext _dbContext;
        public TarefaRepositorio(SistemaTarefasDBContext sistemaTarefasDBContext)
        {
            _dbContext = sistemaTarefasDBContext;
        }

        public async Task<TarefaModel> BurcarTarefaPorId(int id)
        {
            TarefaModel tarefa = await _dbContext.Tarefas
                .Include(x => x.Usuario)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (tarefa == null)
                throw new Exception($"Tarefa com o ID: {id} não foi encontrado no banco de dados!");

            return tarefa;
        }
        public async Task<List<TarefaModel>> BurcarTodasTarefas()
        {
            return await _dbContext.Tarefas
                .Include(x => x.Usuario)
                .ToListAsync();
        }
        public async Task<TarefaModel> AdicionarTarefa(TarefaModel tarefa)
        {
            await _dbContext.Tarefas.AddAsync(tarefa);
            await _dbContext.SaveChangesAsync();

            return tarefa;
        }

        public async Task<TarefaModel> AtualizarTarefa(TarefaModel tarefa, int id)
        {
            TarefaModel tarefaPorId = await BurcarTarefaPorId(id);


            if (tarefa.Nome == null || tarefa.Descricao == null || tarefa.Status == null)
                throw new Exception($"Erro ao atualizar dados da tarefa com o ID: {id}. \n Não é permitido atualizar dados para nulo");

            tarefaPorId.Nome = tarefa.Nome;
            tarefaPorId.Descricao = tarefa.Descricao;
            tarefaPorId.Status = tarefa.Status;
            tarefaPorId.UsuarioId = tarefa.UsuarioId;

            _dbContext.Tarefas.Update(tarefaPorId);
            await _dbContext.SaveChangesAsync();

            return tarefaPorId;

        }
        public async Task<bool> ApagarTarefa(int id)
        {
            TarefaModel tarefaPorId = await BurcarTarefaPorId(id);

            _dbContext.Tarefas.Remove(tarefaPorId);
            await _dbContext.SaveChangesAsync();

            return true;
        }

    }
}

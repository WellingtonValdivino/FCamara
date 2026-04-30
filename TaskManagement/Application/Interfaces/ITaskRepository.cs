using TaskManagement.Application.DTOs.Request;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Application.Interfaces
{
    /// <summary>
    /// Interface para operações de repositório de tarefas.
    /// </summary>
    public interface ITaskRepository
    {
        /// <summary>
        /// Obtém uma tarefa pelo seu identificador.
        /// </summary>
        /// <param name="id">Identificador da tarefa.</param>
        /// <returns>A tarefa correspondente ao identificador, ou null se não encontrada.</returns>
        Task<TaskItem?> GetByIdAsync(Guid id);
        
        /// <summary>
        /// Obtém todas as tarefas com base em um filtro.
        /// </summary>
        /// <param name="filter">Filtro para aplicar na consulta.</param>
        /// <returns>Lista de tarefas que atendem ao filtro.</returns>
        Task<List<TaskItem>> GetAllAsync(TaskFilterRequest filter);
        
        /// <summary>
        /// Adiciona uma nova tarefa.
        /// </summary>
        /// <param name="task">Tarefa a ser adicionada.</param>
        Task AddAsync(TaskItem task);
        
        /// <summary>
        /// Atualiza uma tarefa existente.
        /// </summary>
        /// <param name="task">Tarefa a ser atualizada.</param>
        Task UpdateAsync(TaskItem task);
        
        /// <summary>
        /// Remove uma tarefa existente.
        /// </summary>
        /// <param name="task">Tarefa a ser removida.</param>
        Task DeleteAsync(TaskItem task);
    }
}

using TaskManagement.Application.DTOs.Request;
using TaskManagement.Application.DTOs.Response;

namespace TaskManagement.Application.Interfaces;

/// <summary>
/// Interface para operações de serviço de tarefas, definindo os métodos para criar, obter, atualizar e excluir tarefas.
/// </summary>
public interface ITaskService
{
    /// <summary>
    /// Cria uma nova tarefa.
    /// </summary>
    /// <param name="request">Dados da tarefa a ser criada.</param>
    /// <returns>A tarefa criada.</returns>
    Task<TaskResponse> CreateAsync(CreateTaskRequest request);
    
    /// <summary>
    /// Obtém todas as tarefas com base em um filtro.
    /// </summary>
    /// <param name="filter">Filtro para aplicar na consulta.</param>
    /// <returns>Lista de tarefas que atendem ao filtro.</returns>
    Task<List<TaskResponse>> GetAllAsync(TaskFilterRequest filter);
    
    /// <summary>
    /// Obtém uma tarefa pelo seu identificador.
    /// </summary>
    /// <param name="id">Identificador da tarefa.</param>
    /// <returns>A tarefa correspondente ao identificador, ou null se não encontrada.</returns>
    Task<TaskResponse?> GetByIdAsync(Guid id);
    
    /// <summary>
    /// Atualiza uma tarefa existente.
    /// </summary>
    /// <param name="id">Identificador da tarefa a ser atualizada.</param>
    /// <param name="request">Dados da tarefa a serem atualizados.</param>
    /// <returns>True se a atualização foi bem-sucedida, caso contrário, false.</returns>
    Task<bool> UpdateAsync(Guid id, UpdateTaskRequest request);
    
    /// <summary>
    /// Remove uma tarefa existente.
    /// </summary>
    /// <param name="id">Identificador da tarefa a ser removida.</param>
    /// <returns>True se a remoção foi bem-sucedida, caso contrário, false.</returns>
    Task<bool> DeleteAsync(Guid id);
}
using TaskManagement.Application.DTOs.Request;
using TaskManagement.Application.DTOs.Response;
using TaskManagement.Application.Interfaces;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Application.Services;

/// <summary>
/// Serviço de tarefas que implementa a interface ITaskService e utiliza um repositório para realizar operações de CRUD em tarefas. 
/// Ele mapeia as entidades de domínio para objetos de resposta e vice-versa, garantindo a separação de preocupações entre a camada 
/// de aplicação e a camada de domínio.
/// </summary>
public class TaskService : ITaskService
{
    private readonly ITaskRepository _repository;

    /// <summary>
    /// Construtor do serviço de tarefas que recebe um repositório de tarefas como dependência.
    /// </summary>
    /// <param name="repository"></param>
    public TaskService(ITaskRepository repository)
    {
        _repository = repository;
    }

    /// <summary>
    /// Cria uma nova tarefa com base nos dados fornecidos em CreateTaskRequest, adiciona a tarefa ao repositório e 
    /// retorna um TaskResponse com os detalhes da tarefa criada.
    /// </summary>
    /// <param name="request">Dados da tarefa a ser criada.</param>
    /// <returns>A tarefa criada.</returns>
    public async Task<TaskResponse> CreateAsync(CreateTaskRequest request)
    {
        var task = new TaskItem
        {
            Id = Guid.NewGuid(),
            Title = request.Title,
            Description = request.Description,
            DueDate = request.DueDate,
            Status = request.Status,
            CreatedAt = DateTime.UtcNow
        };

        await _repository.AddAsync(task);

        return MapToResponse(task);
    }

    /// <summary>
    /// Obtém todas as tarefas que correspondem aos critérios de filtro fornecidos em TaskFilterRequest, mapeia cada 
    /// tarefa para um TaskResponse e retorna uma lista de respostas.
    /// </summary>
    /// <param name="filter">Filtro para aplicar na consulta.</param>
    /// <returns>Lista de tarefas que atendem ao filtro.</returns>
    public async Task<List<TaskResponse>> GetAllAsync(TaskFilterRequest filter)
    {
        var tasks = await _repository.GetAllAsync(filter);

        return tasks
            .Select(MapToResponse)
            .ToList();
    }

    /// <summary>
    /// Obtém uma tarefa por seu ID, mapeia a tarefa para um TaskResponse e retorna a resposta. Se a tarefa não for encontrada, retorna null.
    /// </summary>
    /// <param name="id">Identificador da tarefa.</param>
    /// <returns>A tarefa correspondente ao identificador, ou null se não encontrada.</returns>
    public async Task<TaskResponse?> GetByIdAsync(Guid id)
    {
        var task = await _repository.GetByIdAsync(id);

        if (task is null)
            return null;

        return MapToResponse(task);
    }

    /// <summary>
    /// Atualiza uma tarefa existente com base nos dados fornecidos em UpdateTaskRequest.
    /// </summary>
    /// <param name="id">Identificador da tarefa a ser atualizada.</param>
    /// <param name="request">Dados da tarefa a serem atualizados.</param>
    /// <returns>True se a atualização foi bem-sucedida, caso contrário, false.</returns>
    public async Task<bool> UpdateAsync(Guid id, UpdateTaskRequest request)
    {
        var task = await _repository.GetByIdAsync(id);

        if (task is null)
            return false;

        task.Title = request.Title;
        task.Description = request.Description;
        task.DueDate = request.DueDate;
        task.Status = request.Status;
        task.UpdatedAt = DateTime.UtcNow;

        await _repository.UpdateAsync(task);

        return true;
    }
    
    /// <summary>
    /// Remove uma tarefa existente.
    /// </summary>
    /// <param name="id">Identificador da tarefa a ser removida.</param>
    /// <returns>True se a remoção foi bem-sucedida, caso contrário, false.</returns>
    public async Task<bool> DeleteAsync(Guid id)
    {
        var task = await _repository.GetByIdAsync(id);

        if (task is null)
            return false;

        await _repository.DeleteAsync(task);

        return true;
    }

    private static TaskResponse MapToResponse(TaskItem task)
    {
        return new TaskResponse
        {
            Id = task.Id,
            Title = task.Title,
            Description = task.Description,
            DueDate = task.DueDate,
            Status = task.Status,
            CreatedAt = task.CreatedAt
        };
    }
}
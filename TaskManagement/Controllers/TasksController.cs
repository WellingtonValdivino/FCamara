using Microsoft.AspNetCore.Mvc;
using TaskManagement.Application.DTOs;
using TaskManagement.Application.Interfaces;

namespace TaskManagement.Controllers;

/// <summary>
/// Controller responsável por gerenciar as tarefas.
/// </summary>
[ApiController]
[Route("api/tasks")]
public class TasksController : ControllerBase
{
    private readonly ITaskService _taskService;

    /// <summary>
    /// Inicializa uma nova instância da classe TasksController.
    /// </summary>
    /// <param name="taskService">Serviço utilizado para gerenciar as tarefas.</param>
    public TasksController(ITaskService taskService)
    {
        _taskService = taskService;
    }

    /// <summary>
    /// Cria uma nova tarefa.
    /// </summary>
    /// <remarks>
    /// Exemplo de requisição:
    ///
    ///     POST /api/tasks
    ///     {
    ///         "title": "Estudar SOLID",
    ///         "description": "Revisar princípios e exemplos práticos",
    ///         "dueDate": "2026-05-01",
    ///         "status": "Pending"
    ///     }
    ///
    /// </remarks>
    /// <param name="request">Dados necessários para criação da tarefa.</param>
    /// <returns>A tarefa criada com seu identificador único.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(TaskResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateTaskRequest request)
    {
        var task = await _taskService.CreateAsync(request);

        return CreatedAtAction(
            nameof(GetById),
            new { id = task.Id },
            task);
    }

    /// <summary>
    /// Obtém todas as tarefas cadastradas.
    /// </summary>
    /// <remarks>
    /// Permite filtrar as tarefas por status e/ou data de vencimento.
    ///
    /// Exemplo:
    ///
    ///     GET /api/tasks?status=Pending&amp;dueDate=2026-05-01
    ///
    /// </remarks>
    /// <param name="filter">Filtros opcionais para consulta de tarefas.</param>
    /// <returns>Lista de tarefas encontradas. Retorna lista vazia quando não houver registros.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<TaskResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAll([FromQuery] TaskFilterRequest filter)
    {
        var tasks = await _taskService.GetAllAsync(filter);

        return Ok(tasks);
    }

    /// <summary>
    /// Obtém uma tarefa específica pelo identificador.
    /// </summary>
    /// <param name="id">Identificador único da tarefa.</param>
    /// <returns>A tarefa correspondente ao identificador informado.</returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(TaskResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var task = await _taskService.GetByIdAsync(id);

        if (task is null)
            return NotFound();

        return Ok(task);
    }

    /// <summary>
    /// Atualiza uma tarefa existente.
    /// </summary>
    /// <remarks>
    /// Exemplo de requisição:
    ///
    ///     PUT /api/tasks/{id}
    ///     {
    ///         "title": "Estudar REST",
    ///         "description": "Revisar status HTTP e boas práticas",
    ///         "dueDate": "2026-05-02",
    ///         "status": "InProgress"
    ///     }
    ///
    /// </remarks>
    /// <param name="id">Identificador único da tarefa.</param>
    /// <param name="request">Dados atualizados da tarefa.</param>
    /// <returns>Retorna 204 quando a tarefa for atualizada com sucesso.</returns>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateTaskRequest request)
    {
        var updated = await _taskService.UpdateAsync(id, request);

        if (!updated)
            return NotFound();

        return NoContent();
    }

    /// <summary>
    /// Exclui uma tarefa existente.
    /// </summary>
    /// <param name="id">Identificador único da tarefa.</param>
    /// <returns>Retorna 204 quando a tarefa for excluída com sucesso.</returns>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleted = await _taskService.DeleteAsync(id);

        if (!deleted)
            return NotFound();

        return NoContent();
    }
}
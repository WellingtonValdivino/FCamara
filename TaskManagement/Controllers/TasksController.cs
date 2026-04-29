using Microsoft.AspNetCore.Mvc;
using TaskManagement.Application.DTOs;
using TaskManagement.Application.Interfaces;

namespace TaskManagement.Controllers;

[ApiController]
[Route("api/tasks")]
public class TasksController : ControllerBase
{
    private readonly ITaskService _taskService;

    public TasksController(ITaskService taskService)
    {
        _taskService = taskService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTaskRequest request)
    {
        var task = await _taskService.CreateAsync(request);

        return CreatedAtAction(
            nameof(GetById),
            new { id = task.Id },
            task);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] TaskFilterRequest filter)
    {
        var tasks = await _taskService.GetAllAsync(filter);

        return Ok(tasks);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var task = await _taskService.GetByIdAsync(id);

        if (task is null)
            return NotFound();

        return Ok(task);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateTaskRequest request)
    {
        var updated = await _taskService.UpdateAsync(id, request);

        if (!updated)
            return NotFound();

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleted = await _taskService.DeleteAsync(id);

        if (!deleted)
            return NotFound();

        return NoContent();
    }
}
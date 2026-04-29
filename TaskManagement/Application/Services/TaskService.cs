using TaskManagement.Application.DTOs;
using TaskManagement.Application.Interfaces;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Application.Services;

public class TaskService : ITaskService
{
    private readonly ITaskRepository _repository;

    public TaskService(ITaskRepository repository)
    {
        _repository = repository;
    }

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

    public async Task<List<TaskResponse>> GetAllAsync(TaskFilterRequest filter)
    {
        var tasks = await _repository.GetAllAsync(filter);

        return tasks
            .Select(MapToResponse)
            .ToList();
    }

    public async Task<TaskResponse?> GetByIdAsync(Guid id)
    {
        var task = await _repository.GetByIdAsync(id);

        if (task is null)
            return null;

        return MapToResponse(task);
    }

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
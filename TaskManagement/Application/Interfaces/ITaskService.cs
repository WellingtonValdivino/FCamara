using TaskManagement.Application.DTOs;

namespace TaskManagement.Application.Interfaces;

public interface ITaskService
{
    Task<TaskResponse> CreateAsync(CreateTaskRequest request);
    Task<List<TaskResponse>> GetAllAsync(TaskFilterRequest filter);
    Task<TaskResponse?> GetByIdAsync(Guid id);
    Task<bool> UpdateAsync(Guid id, UpdateTaskRequest request);
    Task<bool> DeleteAsync(Guid id);
}
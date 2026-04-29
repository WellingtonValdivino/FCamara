using Microsoft.EntityFrameworkCore;
using TaskManagement.Application.DTOs;
using TaskManagement.Application.Interfaces;
using TaskManagement.Domain.Entities;
using TaskManagement.Infrastructure.Data;

namespace TaskManagement.Infrastructure.Repositories;

public class TaskRepository : ITaskRepository
{
    private readonly AppDbContext _context;

    public TaskRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<TaskItem?> GetByIdAsync(Guid id)
    {
        return await _context.Tasks
            .FirstOrDefaultAsync(task => task.Id == id);
    }

    public async Task<List<TaskItem>> GetAllAsync(TaskFilterRequest filter)
    {
        var query = _context.Tasks.AsQueryable();

        if (filter.Status.HasValue)
            query = query.Where(task => task.Status == filter.Status.Value);

        if (filter.DueDate.HasValue)
            query = query.Where(task => task.DueDate.HasValue &&
                                        task.DueDate.Value.Date == filter.DueDate.Value.Date);

        return await query
            .OrderBy(task => task.DueDate)
            .ThenBy(task => task.Title)
            .ToListAsync();
    }

    public async Task AddAsync(TaskItem task)
    {
        await _context.Tasks.AddAsync(task);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(TaskItem task)
    {
        _context.Tasks.Update(task);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(TaskItem task)
    {
        _context.Tasks.Remove(task);
        await _context.SaveChangesAsync();
    }
}
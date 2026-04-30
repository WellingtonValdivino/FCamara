using Microsoft.EntityFrameworkCore;
using TaskManagement.Application.DTOs.Request;
using TaskManagement.Application.Interfaces;
using TaskManagement.Domain.Entities;
using TaskManagement.Infrastructure.Data;

namespace TaskManagement.Infrastructure.Repositories;

/// <summary>
/// Repositório para gerenciamento de tarefas, implementando as operações de CRUD e consultas específicas.
/// </summary>
public class TaskRepository : ITaskRepository
{
    private readonly AppDbContext _context;

    /// <summary>
    /// Construtor do repositório, recebendo o contexto de banco de dados via injeção de dependência.
    /// </summary>
    /// <param name="context">Contexto de banco de dados.</param>
    public TaskRepository(AppDbContext context)
    {
        _context = context;
    }
    
    /// <summary>
    /// Obtém uma tarefa pelo seu identificador.
    /// </summary>
    /// <param name="id">Identificador da tarefa.</param>
    /// <returns>A tarefa correspondente ao identificador, ou null se não encontrada.</returns>
    public async Task<TaskItem?> GetByIdAsync(Guid id)
    {
        return await _context.Tasks
            .FirstOrDefaultAsync(task => task.Id == id);
    }
    
    /// <summary>
    /// Obtém todas as tarefas, aplicando filtros opcionais.
    /// </summary>
    /// <param name="filter">Filtros para a consulta de tarefas.</param>
    /// <returns>Lista de tarefas que atendem aos filtros.</returns>
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
    
    /// <summary>
    /// Adiciona uma nova tarefa ao banco de dados.
    /// </summary>
    /// <param name="task">Tarefa a ser adicionada.</param>
    public async Task AddAsync(TaskItem task)
    {
        await _context.Tasks.AddAsync(task);
        await _context.SaveChangesAsync();
    }
    
    /// <summary>
    /// Atualiza uma tarefa existente no banco de dados.
    /// </summary>
    /// <param name="task">Tarefa a ser atualizada.</param>
    public async Task UpdateAsync(TaskItem task)
    {
        _context.Tasks.Update(task);
        await _context.SaveChangesAsync();
    }
    
    /// <summary>
    /// Remove uma tarefa do banco de dados.
    /// </summary>
    /// <param name="task">Tarefa a ser removida.</param>
    public async Task DeleteAsync(TaskItem task)
    {
        _context.Tasks.Remove(task);
        await _context.SaveChangesAsync();
    }
}
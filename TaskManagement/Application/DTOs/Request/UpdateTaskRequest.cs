using TaskManagement.Domain.Enums;

namespace TaskManagement.Application.DTOs.Request;

/// <summary>
/// Representa a estrutura de dados usada para atualizar informações de uma tarefa.
/// </summary>
public class UpdateTaskRequest
{
    /// <summary>
    /// Título da tarefa.
    /// </summary>
    public string Title { get; set; } = string.Empty;
    
    /// <summary>
    /// Descrição da tarefa.
    /// </summary>
    public string? Description { get; set; }
    
    /// <summary>
    /// Data de vencimento da tarefa.
    /// </summary>
    public DateTime? DueDate { get; set; }
    
    /// <summary>
    /// Status da tarefa.
    /// </summary>
    public TaskItemStatus Status { get; set; }
}
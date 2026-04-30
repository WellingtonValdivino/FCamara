using TaskManagement.Domain.Enums;

namespace TaskManagement.Application.DTOs.Response;

/// <summary>
/// Representa a estrutura de dados usada para retornar informações sobre uma tarefa em respostas de API.
/// </summary>
public class TaskResponse
{
    /// <summary>
    /// Identificador único da tarefa.
    /// </summary>
    public Guid Id { get; set; }
    
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
    
    /// <summary>
    /// Data de criação da tarefa.
    /// </summary>
    public DateTime CreatedAt { get; set; }
}
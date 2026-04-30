using TaskManagement.Domain.Enums;

namespace TaskManagement.Application.DTOs.Request;

/// <summary>
/// Representa a solicitação para criar uma nova tarefa.
/// </summary>
public class CreateTaskRequest
{
    /// <summary>
    ///  é o título da tarefa. É um campo obrigatório e não pode estar vazio.
    /// </summary>
    public string Title { get; set; } = string.Empty;
    
    /// <summary>
    ///  é a descrição da tarefa. É um campo opcional   .
    /// </summary>
    public string? Description { get; set; }
    
    /// <summary>
    ///  é a data de vencimento da tarefa. É um campo opcional.
    /// </summary>
    public DateTime? DueDate { get; set; }
    
    /// <summary>
    ///  é o status da tarefa. É um campo obrigatório.
    /// </summary>
    public TaskItemStatus Status { get; set; }
}
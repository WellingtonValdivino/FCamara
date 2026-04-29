using TaskManagement.Domain.Enums;

namespace TaskManagement.Application.DTOs;

public class TaskFilterRequest
{
    /// <summary>
    /// Filtra tarefas por status. Valores aceitos: Pending, InProgress ou Completed.
    /// </summary>
    public TaskItemStatus? Status { get; set; }

    /// <summary>
    /// Filtra tarefas pela data de vencimento no formato yyyy-MM-dd.
    /// </summary>
    public DateTime? DueDate { get; set; }
}
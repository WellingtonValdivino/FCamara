using TaskManagement.Domain.Enums;

namespace TaskManagement.Application.DTOs.Request;

/// <summary>
/// Representa os critérios usados para filtrar tarefas em uma operação de consulta.
/// </summary>
/// <remarks>Use esta classe para especificar opções de filtragem, como status da tarefa ou data de vencimento, ao recuperar uma lista de
/// tarefas. Todas as propriedades são opcionais; defina apenas os filtros relevantes para sua consulta.</remarks>
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
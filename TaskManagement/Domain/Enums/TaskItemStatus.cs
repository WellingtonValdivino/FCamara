using System.ComponentModel;

namespace TaskManagement.Domain.Enums
{
    /// <summary>
    /// Enumeração para representar o status de um item de tarefa.
    /// </summary>
    public enum TaskItemStatus
    {
        /// <summary>
        /// status pendente, indicando que a tarefa ainda não foi iniciada ou está aguardando para ser iniciada.
        /// </summary>
        [Description("Pendente")]
        Pending = 1,
        
        /// <summary>
        /// status em andamento, indicando que a tarefa está atualmente sendo trabalhada.
        /// </summary>
        [Description("Em andamento")]
        InProgress = 2,
        
        /// <summary>
        /// status concluído, indicando que a tarefa foi finalizada com sucesso.
        /// </summary>
        [Description("Concluída")]
        Completed = 3
    }
}

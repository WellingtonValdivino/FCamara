using TaskManagement.Domain.Enums;

namespace TaskManagement.Domain.Entities
{
    /// <summary>
    /// Entidade que representa um item de tarefa no sistema de gerenciamento de tarefas.
    /// </summary>
    public class TaskItem
    {
        /// <summary>
        /// é o identificador único da tarefa, do tipo Guid, que é gerado automaticamente para garantir a unicidade de cada tarefa no sistema.
        /// </summary>
        public Guid Id { get; set; }
        
        /// <summary>
        /// é o título da tarefa, que é obrigatório e não pode exceder 100 caracteres.
        /// </summary>
        public string Title { get; set; } = string.Empty;
        
        /// <summary>
        /// é a descrição da tarefa, que é opcional e não pode exceder 500 caracteres.
        /// </summary>
        public string? Description { get; set; }
        
        /// <summary>
        /// é a data de vencimento da tarefa, que é opcional e não pode estar no passado.
        /// </summary>
        public DateTime? DueDate { get; set; }
        
        /// <summary>
        /// é o status da tarefa, que indica o estado atual da tarefa no sistema.
        /// </summary>
        public TaskItemStatus Status { get; set; }
        
        /// <summary>
        /// é a data de criação da tarefa, que é gerada automaticamente quando a tarefa é criada.
        /// </summary>
        public DateTime CreatedAt { get; set; }
        
        /// <summary>
        /// é a data de atualização da tarefa, que é gerada automaticamente quando a tarefa é atualizada.
        /// </summary>
        public DateTime? UpdatedAt { get; set; }
    }
}

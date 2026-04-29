using System.ComponentModel;

namespace TaskManagement.Domain.Enums
{
    public enum TaskItemStatus
    {
        [Description("Pendente")]
        Pending = 1,
        [Description("Em andamento")]
        InProgress = 2,
        [Description("Concluída")]
        Completed = 3
    }
}

using FluentValidation;
using TaskManagement.Application.DTOs.Request;
using TaskManagement.Domain.Enums;

namespace TaskManagement.Application.Validators;

/// <summary>
/// Validador para a requisição de criação de tarefa, garantindo que os dados fornecidos estejam corretos e completos antes de serem processados.
/// </summary>
public class CreateTaskRequestValidator : AbstractValidator<CreateTaskRequest>
{
    /// <summary>
    /// Inicializa uma nova instância da classe CreateTaskRequestValidator, definindo regras de validação para a criação de uma solicitação de tarefa.
    /// </summary>
    /// <remarks>O validador garante que o título seja obrigatório e não exceda 100 caracteres, a descrição não exceda 500 caracteres, o status seja 
    /// um valor TaskItemStatus válido e a data de vencimento não esteja definida no passado. Essas regras ajudam a garantir que as solicitações de 
    /// criação de tarefas atendam aos requisitos de dados esperados antes do processamento.
    /// </remarks>
    public CreateTaskRequestValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .WithMessage("É necessário um título.")
            .MaximumLength(100)
            .WithMessage("O título não deve exceder 100 caracteres.");

        RuleFor(x => x.Description)
            .MaximumLength(500)
            .WithMessage("A descrição não deve exceder 500 caracteres.");

        RuleFor(x => x.Status)
            .Must(status => Enum.IsDefined(typeof(TaskItemStatus), status))
            .WithMessage("Status da tarefa inválido.");

        RuleFor(x => x.DueDate)
            .GreaterThanOrEqualTo(DateTime.Today)
            .When(x => x.DueDate.HasValue)
            .WithMessage("A data de vencimento não pode ser anterior ao passado.");
    }
}
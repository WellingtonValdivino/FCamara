using FluentValidation;
using TaskManagement.Application.DTOs.Request;
using TaskManagement.Domain.Enums;

namespace TaskManagement.Application.Validators;

/// <summary>
/// Validador para a requisição de atualização de tarefa, garantindo que os dados fornecidos estejam corretos e completos antes de serem processados.
/// </summary>
public class UpdateTaskRequestValidator : AbstractValidator<UpdateTaskRequest>
{
    /// <summary>
    /// Fornece regras de validação para atualizar uma solicitação de tarefa.
    /// </summary>
    /// <remarks>Este validador impõe restrições aos campos de título, descrição, status e data de vencimento para garantir que as solicitações de
    /// atualização atendam às regras de negócio necessárias. As falhas de validação incluirão mensagens de erro descritivas para cada campo inválido.
    /// </remarks>
    public UpdateTaskRequestValidator()
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
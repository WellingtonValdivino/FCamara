using FluentValidation;
using TaskManagement.Application.DTOs;
using TaskManagement.Domain.Enums;

namespace TaskManagement.Application.Validators;

public class CreateTaskRequestValidator : AbstractValidator<CreateTaskRequest>
{
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
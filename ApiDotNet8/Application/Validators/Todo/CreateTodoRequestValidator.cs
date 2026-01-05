using ApiDotNet8.Application.DTOs.Todo;
using FluentValidation;

namespace ApiDotNet8.Application.Validators.Todo;

public class CreateTodoRequestValidator : AbstractValidator<CreateTodoRequest>
{
    public CreateTodoRequestValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required")
            .MinimumLength(3).WithMessage("Title must have at least 3 characters")
            .MaximumLength(200).WithMessage("Title must have at most 200 characters");
    }
}

using FluentValidation;
using FluentValidation.Validators;

namespace TaskManager.Contracts.Tasks;

public class TaskRequestValidator : AbstractValidator<TaskRequest>
{
    public TaskRequestValidator()
    {
        RuleFor(x=> x.Name).NotEmpty().Length(3,100);
        RuleFor(x=> x.Description).NotEmpty().Length(3,2500);
        RuleFor(x => x.StartDate).NotEmpty().GreaterThanOrEqualTo(DateOnly.FromDateTime(DateTime.UtcNow));
        RuleFor(x => x.EndDate ).NotEmpty().GreaterThan(x=>x.StartDate).WithMessage("{PropertyName} must be greater than or equal start date"); ;
    }
}

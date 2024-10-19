using FluentValidation;
using FluentValidation.Validators;

namespace TaskManager.Contracts.TeamMembers;

public class TeamMembersRequestValidator : AbstractValidator<TeamMembersRequest>
{
    public TeamMembersRequestValidator()
    {
        RuleFor(x=> x.Name).Length(3,100);
        RuleFor(x=> x.Email).Length(3,50).EmailAddress();
    }
}

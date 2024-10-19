
using TaskManager.Abstractions;

namespace TaskManager.Errors;

public static class TeamMemberErrors
{
    public static readonly Error TeamMemberNotFound = new("TeamMember.TaskNotFound", "No Team Members was found with the given id", StatusCodes.Status404NotFound);
    public static readonly Error DuplicateTeamMember = new("TeamMember.Duplicated", "There is another user with same email already exist!", StatusCodes.Status400BadRequest);

}

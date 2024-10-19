using TaskManager.Contracts.Tasks;
namespace TaskManager.Contracts.TeamMembers;

public record TeamMembersResponse(
    int Id,
    string Name,
    string Email,
    IEnumerable<TaskResponse> Tasks
   
    );


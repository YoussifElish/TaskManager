using TaskManager.Contracts.Tasks;

namespace TaskManager.Contracts.TeamMembers;

public record TeamMembersRequest(
  string Name,
    string Email
    );


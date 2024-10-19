using TaskManager.Abstractions;
using TaskManager.Contracts.Tasks;
using TaskManager.Contracts.TeamMembers;

namespace TaskManager.Services;

public interface ITeamMemberService
{
    Task<IEnumerable<TeamMembersResponse>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Result<TeamMembersResponse>> GetAsync(int id, CancellationToken cancellationToken = default);
    Task<Result<TeamMembersResponse>> AddAsync(TeamMembersRequest request, CancellationToken cancellationToken = default);
    Task<Result> UpdateAsync(int id, TeamMembersRequest request, CancellationToken cancellationToken = default);
    Task<Result> DeleteAsync(int id, CancellationToken cancellationToken = default);
}

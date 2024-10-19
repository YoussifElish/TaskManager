using Mapster;
using Microsoft.EntityFrameworkCore;
using TaskManager.Abstractions;
using TaskManager.Contracts.Tasks;
using TaskManager.Contracts.TeamMembers;
using TaskManager.Entities;
using TaskManager.Errors;
using TaskManager.Persistence;

namespace TaskManager.Services;

public class TeamMemberService(ApplicationDbContext context): ITeamMemberService
{
    private readonly ApplicationDbContext _context = context;

    public async Task<IEnumerable<TeamMembersResponse>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var tasks = await _context.TeamMembers.Where
            (x => !x.IsDeleted)
            .Include(x=>x.Tasks)
            .Select(t=> 
                new TeamMembersResponse 
                (t.Id,
                t.Name,
                t.Email,
                t.Tasks.Where(x=> !x.IsDeleted).Select
                    (t=> new TaskResponse 
                    (t.Id,t.Name,t.Description,t.IsDone,t.StartDate,t.EndDate,t.TeamMemberId))
            .ToList()))
            .ToListAsync(cancellationToken);

        return tasks;

    }

    public async Task<Result<TeamMembersResponse>> GetAsync(int id, CancellationToken cancellationToken = default)
    {
        var isExist = await _context.TeamMembers.AnyAsync(x => x.Id == id);
        if (!isExist)
            return Result.Failure<TeamMembersResponse>(TeamMemberErrors.TeamMemberNotFound);
        var members = await _context.TeamMembers.Where
           (x => x.Id == id &&!x.IsDeleted )
           .Include(x => x.Tasks)
           .Select(t =>
               new TeamMembersResponse
               (t.Id,
               t.Name,
               t.Email,
               t.Tasks.Where(x => !x.IsDeleted).Select
                   (t => new TaskResponse
                   (t.Id, t.Name, t.Description, t.IsDone, t.StartDate, t.EndDate, t.TeamMemberId))
           .ToList()))
           .SingleOrDefaultAsync(cancellationToken);

        return Result.Success<TeamMembersResponse>(members!);
    }


    public async Task<Result<TeamMembersResponse>> AddAsync(TeamMembersRequest request, CancellationToken cancellationToken = default)
    {

        var isExistingEmail = await _context.TeamMembers.AnyAsync(x => x.Email == request.Email, cancellationToken);
        if (isExistingEmail)
            return Result.Failure<TeamMembersResponse>(TeamMemberErrors.DuplicateTeamMember);

        var result = request.Adapt<TeamMember>();
        await _context.AddAsync(result, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success<TeamMembersResponse>(result.Adapt<TeamMembersResponse>());
    }

    public async Task<Result> UpdateAsync(int id, TeamMembersRequest request, CancellationToken cancellationToken = default)
    {
        if (await _context.TeamMembers.AnyAsync(x => x.Id != id && x.Email == request.Email, cancellationToken))
            return Result.Failure(TeamMemberErrors.DuplicateTeamMember);

        if (await _context.TeamMembers.Where(x => x.Id == id && !x.IsDeleted).SingleOrDefaultAsync() is not { } member)
            return Result.Failure(TeamMemberErrors.TeamMemberNotFound);

        member.Name = request.Name;
        member.Email = request.Email;
        
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }


    // I've implemented a soft delete here, and I didn't allow the user to change the status from 'deleted' back to 'not deleted'; they can only change it from 'not deleted' to 'deleted

    public async Task<Result> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        if (await _context.TeamMembers.Where(x => x.Id == id && !x.IsDeleted).SingleOrDefaultAsync() is not { } member)
            return Result.Failure<TaskResponse>(TeamMemberErrors.TeamMemberNotFound);
        member.IsDeleted = true;

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}

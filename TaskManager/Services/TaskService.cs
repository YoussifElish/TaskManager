using Mapster;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using TaskManager.Abstractions;
using TaskManager.Contracts.Tasks;
using TaskManager.Entities;
using TaskManager.Errors;
using TaskManager.Persistence;

namespace TaskManager.Services;

public class TaskService(ApplicationDbContext context) : ITaskService
{
    private readonly ApplicationDbContext _context = context;


    public async Task<IEnumerable<TaskResponse>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var tasks = await _context.Tasks.Where(x=>!x.IsDeleted).AsNoTracking().ProjectToType<TaskResponse>().ToListAsync(cancellationToken);

        return tasks;
    
    }


    public async Task<Result<TaskResponse>> GetAsync(int id , CancellationToken cancellationToken = default)
    {

        if (await _context.Tasks.Where(x=> x.Id == id && !x.IsDeleted).SingleOrDefaultAsync() is not { } tasks)
            return Result.Failure<TaskResponse>(TaskErrors.TaskNotFound);

        return Result.Success<TaskResponse>(tasks.Adapt<TaskResponse>());
    }
        
    public async Task<Result<TaskResponse>> AddAsync(TaskRequest request, CancellationToken cancellationToken = default)
    {

        var isExistingName = await _context.Tasks.AnyAsync(x => x.Name == request.Name, cancellationToken);
        if (isExistingName)
          return  Result.Failure<TaskResponse>(TaskErrors.DuplicateTask);

        if (await _context.TeamMembers.FindAsync(request.TeamMemberId)  is not { } member)
            return Result.Failure<TaskResponse>(TeamMemberErrors.TeamMemberNotFound);
        var result = request.Adapt<Entities.Task>();
        await _context.AddAsync(result, cancellationToken);
         await _context.SaveChangesAsync(cancellationToken);

        return Result.Success<TaskResponse>(result.Adapt<TaskResponse>());
    }

    public async Task<Result> UpdateAsync(int id ,TaskRequest request, CancellationToken cancellationToken = default)
    {
        if (await _context.Tasks.AnyAsync(x => x.Id != id && x.Name == request.Name, cancellationToken))
            return  Result.Failure(TaskErrors.DuplicateTask);

        if (await _context.Tasks.Where(x => x.Id == id && !x.IsDeleted).SingleOrDefaultAsync() is not { } task)     
            return Result.Failure<TaskResponse>(TaskErrors.TaskNotFound);

        if (await _context.TeamMembers.FindAsync(request.TeamMemberId) is not { } member)
            return Result.Failure<TaskResponse>(TeamMemberErrors.TeamMemberNotFound);

        task.Name = request.Name;
         task.Description = request.Description;
        task.StartDate = request.StartDate;
        task.EndDate = request.EndDate;
        task.TeamMemberId = request.TeamMemberId;
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }


    // I've implemented a soft delete here, and I didn't allow the user to change the status from 'deleted' back to 'not deleted'; they can only change it from 'not deleted' to 'deleted

    public async Task<Result> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        if (await _context.Tasks.Where(x => x.Id == id && !x.IsDeleted).SingleOrDefaultAsync() is not { } task)
            return Result.Failure<TaskResponse>(TaskErrors.TaskNotFound);
        task.IsDeleted = true;  

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}

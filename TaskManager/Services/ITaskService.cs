using TaskManager.Abstractions;
using TaskManager.Contracts.Tasks;

namespace TaskManager.Services;

public interface ITaskService
{
    Task<IEnumerable<TaskResponse>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Result<TaskResponse>> GetAsync(int id, CancellationToken cancellationToken = default);
    Task<Result<TaskResponse>> AddAsync(TaskRequest request, CancellationToken cancellationToken = default);
    Task<Result> UpdateAsync(int id, TaskRequest request, CancellationToken cancellationToken = default);
    Task<Result> DeleteAsync(int id, CancellationToken cancellationToken = default);
}

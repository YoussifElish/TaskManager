
using TaskManager.Abstractions;

namespace TaskManager.Errors;

public static class TaskErrors
{
    public static readonly Error TaskNotFound = new("Task.TaskNotFound", "no Task was found with the given id", StatusCodes.Status404NotFound);
    public static readonly Error DuplicateTask = new("Task.DuplicateTask", "There is amother task already exist with the same name!", StatusCodes.Status400BadRequest);

}

namespace TaskManager.Contracts.Tasks;

public record TaskResponse(
    int Id,
    string Name,
    string Description,
    bool Status,
    DateOnly StartDate,
    DateOnly EndDate,
    int TeamMemberId
    );


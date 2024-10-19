namespace TaskManager.Contracts.Tasks;

public record TaskRequest(
    string Name,
    string Description,
    string Status,
    DateOnly StartDate,
    DateOnly EndDate,
    int TeamMemberId
    );


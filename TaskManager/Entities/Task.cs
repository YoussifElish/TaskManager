namespace TaskManager.Entities;

public sealed class Task
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool IsDone { get; set; } =  false; // status 
    public DateOnly StartDate { get; set; }  
    public DateOnly EndDate { get; set; }
    public int TeamMemberId { get; set; }
    public TeamMember TeamMember { get; set; } = default!;
    public bool IsDeleted { get; set; } = false; 


}

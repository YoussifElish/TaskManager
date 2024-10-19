namespace TaskManager.Entities;

public sealed class TeamMember
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public ICollection<Task> Tasks { get; set; } = [];
    public bool IsDeleted { get; set; } = false; // status 

}

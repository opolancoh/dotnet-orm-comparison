namespace DotNetOrmComparison.Core.Entities;

public class Project: BaseEntity
{
    public string Name { get; init; }
    public ProjectStatus Status { get; init; }
    public int Progress { get; init; }
    
    // Navigation property
    public ICollection<EmployeeProject> EmployeeProjects { get; set; } = new List<EmployeeProject>();
}

public enum ProjectStatus
{
    NotStarted=1,
    InProgress,
    Completed,
    Canceled
}
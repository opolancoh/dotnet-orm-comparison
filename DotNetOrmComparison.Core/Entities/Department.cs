namespace DotNetOrmComparison.Core.Entities;

public class Department : BaseEntity
{
    public string Name { get; init; }
    public string Manager { get; init; }
    
    // Navigation property
    public ICollection<Employee> Employees { get; set; }
}
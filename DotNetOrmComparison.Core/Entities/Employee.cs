namespace DotNetOrmComparison.Core.Entities;

public class Employee : BaseEntity
{
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public Gender Gender { get; init; }
    public string Email { get; init; }
    public DateTime HireDate { get; init; }
    public decimal Salary { get; init; }
    public bool IsActive { get; init; }
    public int HourlyRate { get; init; }
    public MaritalStatus MaritalStatus { get; init; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    // One-to-One relationship with EmployeeDetail
    // Navigation property
    public Address Address { get; set; }

    // One-to-Many relationship with Department
    // Foreign key
    public Guid DepartmentId { get; set; }

    // Navigation property
    public Department Department { get; set; }

    // Many-to-Many relationship with Project
    // Navigation property
    public ICollection<EmployeeProject> EmployeeProjects { get; set; } = new List<EmployeeProject>();
}

public enum Gender
{
    Male = 1,
    Female,
}

public enum MaritalStatus
{
    Single = 1,
    Married,
    Divorced,
}
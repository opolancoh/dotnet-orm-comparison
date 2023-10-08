namespace DotNetOrmComparison.Core.Entities;

public class Address: BaseEntity
{
    public string Street { get; init; }
    public string City { get; init; }
    public string State { get; init; }
    public string ZipCode { get; init; }
    
    // Foreign key
    public Guid EmployeeId { get; set; }
    // Navigation property
    public Employee Employee { get; set; }
}
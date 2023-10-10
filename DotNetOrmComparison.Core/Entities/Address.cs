namespace DotNetOrmComparison.Core.Entities;

public class Address
{
    public string Street { get; init; }
    public string City { get; init; }
    public string State { get; init; }
    public string ZipCode { get; init; }
    
    // Key and Foreign key
    public Guid EmployeeId { get; set; }
    // Navigation property
    public Employee Employee { get; set; }
}
using DotNetOrmComparison.Core.Entities;

namespace DotNetOrmComparison.Core.Shared.DTOs;

public record EmployeeDetailResult
{
    public Guid Id { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public int Gender { get; init; }
    public string Email { get; init; }
    public DateTime HireDate { get; init; }
    public decimal Salary { get; init; }
    public bool IsActive { get; init; }
    public int HourlyRate { get; init; }
    public MaritalStatus MaritalStatus { get; init; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    
    public AddressDto Address { get; set; }
    public KeyValueDto<Guid> Department { get; set; }
    public List<KeyValueDto<Guid>> Projects { get; set; }
}

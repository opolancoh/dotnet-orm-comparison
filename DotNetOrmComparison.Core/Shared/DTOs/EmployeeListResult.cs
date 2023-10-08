namespace DotNetOrmComparison.Core.Shared.DTOs;

public record EmployeeListResult
{
    public Guid Id { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public int Gender { get; init; }
    public string Email { get; init; }
    public decimal Salary { get; init; }
    
    public AddressShortDto Address { get; set; }
    public KeyValueDto<Guid> Department { get; set; }
    public List<KeyValueDto<Guid>> Projects { get; set; }
}

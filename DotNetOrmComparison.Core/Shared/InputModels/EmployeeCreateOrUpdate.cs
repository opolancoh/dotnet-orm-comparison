using DotNetOrmComparison.Core.Entities;
using DotNetOrmComparison.Core.Shared.DTOs;

namespace DotNetOrmComparison.Core.Shared.InputModels;

public record EmployeeCreateOrUpdate
{
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public Gender Gender { get; init; }
    public string Email { get; init; }
    public DateTime HireDate { get; init; }
    public decimal Salary { get; init; }
    public int HourlyRate { get; init; }
    public MaritalStatus MaritalStatus { get; init; }
    public AddressDto Address { get; set; }
    public Guid DepartmentId { get; init; }
    public List<Guid>? Projects { get; init; }
}
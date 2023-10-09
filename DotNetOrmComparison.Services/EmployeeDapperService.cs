using DotNetOrmComparison.Core.Contracts.Repositories;
using DotNetOrmComparison.Core.Contracts.Services;
using DotNetOrmComparison.Core.Entities;
using DotNetOrmComparison.Core.Shared.InputModels;
using DotNetOrmComparison.Core.Shared.DTOs;

namespace DotNetOrmComparison.Services;

public class EmployeeDapperService : IEmployeeDapperService
{
    private readonly IEmployeeDapperRepository _repository;

    public EmployeeDapperService(IEmployeeDapperRepository repository)
    {
        _repository = repository;
    }

    public async Task<ApplicationResult> GetAll(PaginationOptions pagination)
    {
        var result = await _repository.GetAll(pagination);

        return new ApplicationResult
        {
            Status = 200,
            PageInfo = result.PageInfo,
            Data = result.Data
        };
    }

    public async Task<ApplicationResult?> GetById(Guid id)
    {
        var item = await _repository.GetById(id);

        if (item == null)
        {
            return new ApplicationResult
            {
                Status = 404,
                Message = $"The item with id '{id}' was not found or you don't have permission to access it."
            };
        }

        return new ApplicationResult
        {
            Status = 200,
            Data = item
        };
    }

    public async Task<ApplicationResult> Add(EmployeeCreateOrUpdate item)
    {
        var itemId = Guid.NewGuid();
        var newItem = new Employee
        {
            Id = itemId,
            FirstName = item.FirstName,
            LastName = item.LastName,
            Gender = item.Gender,
            Email = item.Email,
            HireDate = item.HireDate,
            Salary = item.Salary,
            IsActive = true,
            HourlyRate = item.HourlyRate,
            MaritalStatus = item.MaritalStatus,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            Address = new Address
            {
                Id = Guid.NewGuid(),
                Street = item.Address.Street,
                City = item.Address.City,
                State = item.Address.State,
                ZipCode = item.Address.ZipCode,
                EmployeeId = itemId
            },
            DepartmentId = item.DepartmentId
        };

        if (item.Projects != null)
        {
            foreach (var projectId in item.Projects)
            {
                newItem.EmployeeProjects.Add(new EmployeeProject { EmployeeId = newItem.Id, ProjectId = projectId });
            }
        }

        var isValid = await _repository.Add(newItem);

        if (isValid)
        {
            return new ApplicationResult
            {
                Status = 201,
                Message = "Item created successfully.",
                Data = new { newItem.Id }
            };
        }

        return new ApplicationResult
        {
            Status = 400,
            Message = "The item could not be created."
        };
    }
}
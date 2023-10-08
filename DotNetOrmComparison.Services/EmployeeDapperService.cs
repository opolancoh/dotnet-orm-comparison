using DotNetOrmComparison.Core.Contracts.Repositories;
using DotNetOrmComparison.Core.Contracts.Services;
using DotNetOrmComparison.Core.Shared.InputModels;
using DotNetOrmComparison.Core.Shared.DTOs;

namespace DotNetOrmComparison.Services;

public class EmployeeDapperService : IEmployeeDapperService
{
    private readonly IEmployeeDapperRepository _employeeRepository;

    public EmployeeDapperService(IEmployeeDapperRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public async Task<ApplicationResult> GetAll(PaginationOptions pagination)
    {
        var result = await _employeeRepository.GetAll(pagination);

        return new ApplicationResult
        {
            Status = 200,
            PageInfo = result.PageInfo,
            Data = result.Data
        };
    }

    public Task<ApplicationResult> GetById(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<ApplicationResult> Create(EmployeeCreateOrUpdate item)
    {
        throw new NotImplementedException();
    }

    public Task<ApplicationResult> Update(Guid id, EmployeeCreateOrUpdate item)
    {
        throw new NotImplementedException();
    }

    public Task<ApplicationResult> Remove(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<ApplicationResult> AddRange(IEnumerable<EmployeeCreateOrUpdate> items)
    {
        throw new NotImplementedException();
    }
}
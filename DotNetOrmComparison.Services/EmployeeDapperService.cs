using DotNetOrmComparison.Core.Contracts.Repositories;
using DotNetOrmComparison.Core.Contracts.Services;
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
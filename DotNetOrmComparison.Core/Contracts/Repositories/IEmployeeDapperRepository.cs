using DotNetOrmComparison.Core.Entities;
using DotNetOrmComparison.Core.Shared.DTOs;
using DotNetOrmComparison.Core.Shared.InputModels;

namespace DotNetOrmComparison.Core.Contracts.Repositories;

public interface IEmployeeDapperRepository
{
    Task<PagedListResult<EmployeeListResult>> GetAll(PaginationOptions pagination);
    Task<EmployeeDetailResult?> GetById(Guid id);
    Task<bool> Add(Employee item);
}
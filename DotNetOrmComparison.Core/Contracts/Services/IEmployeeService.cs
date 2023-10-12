using DotNetOrmComparison.Core.Shared.InputModels;
using DotNetOrmComparison.Core.Shared.DTOs;

namespace DotNetOrmComparison.Core.Contracts.Services;

public interface IEmployeeService
{
    Task<ApplicationResult> GetAll(PaginationOptions pagination);
    Task<ApplicationResult?> GetById(Guid id);
    Task<ApplicationResult> Add(EmployeeCreateOrUpdate item);
}
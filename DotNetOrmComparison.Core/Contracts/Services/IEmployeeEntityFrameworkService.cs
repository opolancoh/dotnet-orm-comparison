using DotNetOrmComparison.Core.Shared.InputModels;
using DotNetOrmComparison.Core.Shared.DTOs;

namespace DotNetOrmComparison.Core.Contracts.Services;

public interface IEmployeeEntityFrameworkService
{
    Task<ApplicationResult> GetAll(PaginationOptions pagination);
    public Task<ApplicationResult> AddRange(IEnumerable<EmployeeCreateOrUpdate> items);
}
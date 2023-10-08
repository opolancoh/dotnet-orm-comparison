using DotNetOrmComparison.Core.Shared.DTOs;
using DotNetOrmComparison.Core.Shared.InputModels;

namespace DotNetOrmComparison.Core.Contracts.Repositories;

public interface IEmployeeEntityFrameworkRepository // : IRepositoryBase<Employee, Guid>
{
    Task<PagedListResult<EmployeeListResult>> GetAll(PaginationOptions pagination);
    // Task<RiskDto?> GetById(Guid id);
    // Task Create(Risk item);
    // Task Update(Risk item);
    // Task Remove(Guid id);
    // Task<bool> ItemExists(Guid id);
    // Task AddRange(IEnumerable<Risk> items);
}
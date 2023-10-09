using DotNetOrmComparison.Core.Contracts.Repositories;
using DotNetOrmComparison.Core.Entities;
using DotNetOrmComparison.Core.Shared.DTOs;
using DotNetOrmComparison.Core.Shared.InputModels;
using Microsoft.EntityFrameworkCore;

namespace DotNetOrmComparison.Data.EntityFramework;

public class EmployeeEntityFrameworkRepository : IEmployeeEntityFrameworkRepository
{
    private readonly AppDbContext _context;
    private readonly DbSet<Employee> _entitySet;

    public EmployeeEntityFrameworkRepository(AppDbContext context)
    {
        _context = context;
        _entitySet = context.Employees;
    }

    public async Task<PagedListResult<EmployeeListResult>> GetAll(PaginationOptions pagination)
    {
        var (pageIndex, pageSize, skip, addTotalCount) = pagination;

        // Get the total count of records
        int? totalCount = null;
        if (addTotalCount)
        {
            totalCount = await _entitySet.CountAsync();
        }

        var data = await _entitySet
            .AsNoTracking()
            .Skip(skip)
            .Take(pageSize)
            .Select(
                x => new EmployeeListResult
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Gender = (int)x.Gender,
                    Email = x.Email,
                    Salary = x.Salary,
                    Address = new AddressShortDto
                    {
                        Street = x.Address.Street,
                        City = x.Address.City,
                        State = x.Address.State
                    },
                    Department = new KeyValueDto<Guid>
                    {
                        Id = x.Department.Id,
                        Name = x.Department.Name
                    },
                    Projects = x.EmployeeProjects.Select(p => new KeyValueDto<Guid>
                    {
                        Id = p.ProjectId,
                        Name = p.Project.Name
                    }).ToList()
                })
            .ToListAsync();

        return new PagedListResult<EmployeeListResult>
        {
            Data = data,
            PageInfo = new PageInfo
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalCount = totalCount
            }
        };
    }

    public async Task<EmployeeDetailResult?> GetById(Guid id)
    {
        return await _entitySet
            .AsNoTracking()
            .Select(
                x => new EmployeeDetailResult
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Gender = (int)x.Gender,
                    Email = x.Email,
                    HireDate = x.HireDate,
                    Salary = x.Salary,
                    IsActive = x.IsActive,
                    HourlyRate = x.HourlyRate,
                    MaritalStatus = x.MaritalStatus,
                    CreatedAt = x.CreatedAt,
                    UpdatedAt = x.UpdatedAt,
                    Address = new AddressDto
                    {
                        Street = x.Address.Street,
                        City = x.Address.City,
                        State = x.Address.State,
                        ZipCode = x.Address.ZipCode
                    },
                    Department = new KeyValueDto<Guid>
                    {
                        Id = x.Department.Id,
                        Name = x.Department.Name
                    },
                    Projects = x.EmployeeProjects.Select(p => new KeyValueDto<Guid>
                    {
                        Id = p.ProjectId,
                        Name = p.Project.Name
                    }).ToList()
                })
            .SingleOrDefaultAsync(x => x.Id == id);
    }

    public async Task<bool> Add(Employee item)
    {
        _entitySet.Add(item);
        var affectedRows = await _context.SaveChangesAsync();
        return affectedRows > 0;
    }

    public Task<int> Update(Employee item)
    {
        throw new NotImplementedException();
    }

    public Task<int> Remove(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ItemExists(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task AddRange(IEnumerable<Employee> items)
    {
        throw new NotImplementedException();
    }
}
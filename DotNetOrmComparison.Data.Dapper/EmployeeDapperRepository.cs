using System.Data;
using Dapper;
using DotNetOrmComparison.Core.Contracts.Repositories;
using DotNetOrmComparison.Core.Shared.DTOs;
using DotNetOrmComparison.Core.Shared.InputModels;

namespace DotNetOrmComparison.Data.Dapper;

public class EmployeeDapperRepository : IEmployeeDapperRepository
{
    private readonly IDbConnection _dbConnection;

    public EmployeeDapperRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public async Task<PagedListResult<EmployeeListResult>> GetAll(PaginationOptions pagination)
    {
        var (pageIndex, pageSize, skip, addTotalCount) = pagination;

        // Get the total count of records
        int? totalCount = null;
        if (addTotalCount)
        {
            const string countQuery = "SELECT COUNT(*) FROM Employees";
            totalCount = await _dbConnection.ExecuteScalarAsync<int>(countQuery);
        }

        const string sql = """
                           SELECT
                            e."Id", e."FirstName", e."LastName", e."Gender"::int, e."Email", e."Salary",
                            a."Street", a."City", a."State",
                            d."Id", d."Name",
                            p."Id", p."Name"
                           FROM (
                            SELECT e0."Id", e0."DepartmentId", e0."Email", e0."FirstName", e0."Gender", e0."LastName", e0."Salary"
                            FROM "Employees" AS e0
                            LIMIT @PageSize OFFSET @Skip
                           ) AS e
                           LEFT JOIN "Addresses" AS a ON e."Id" = a."EmployeeId"
                           LEFT JOIN "Departments" AS d ON e."DepartmentId" = d."Id"
                           LEFT JOIN (
                            SELECT p0."Id", p0."Name", ep."EmployeeId"
                               FROM "EmployeeProjects" AS ep
                               INNER JOIN "Projects" AS p0 ON ep."ProjectId" = p0."Id"
                           ) AS p ON e."Id" = p."EmployeeId"
                           ORDER BY e."Id"
                           """;

        var lookup = new Dictionary<Guid, EmployeeListResult>();

        await _dbConnection
            .QueryAsync<EmployeeListResult, AddressShortDto, KeyValueDto<Guid>, KeyValueDto<Guid>, EmployeeListResult>(
                sql,
                (employee, address, department, project) =>
                {
                    if (!lookup.TryGetValue(employee.Id, out var employeeEntry))
                    {
                        lookup.Add(employee.Id, employeeEntry = employee);
                        employeeEntry.Address = address;
                        employeeEntry.Department = department;
                        employeeEntry.Projects = new List<KeyValueDto<Guid>>();
                    }

                    if (project != null)
                    {
                        employeeEntry.Projects.Add(project);
                    }

                    return employeeEntry;
                },
                new { Skip = skip, PageSize = pageSize },
                splitOn: "Street,Id,Id" // Split on Address.Street, DepartmentId, ProjectId
            );

        return new PagedListResult<EmployeeListResult>
        {
            Data = lookup.Values,
            PageInfo = new PageInfo
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalCount = totalCount
            }
        };
    }
}
using Dapper;
using DotNetOrmComparison.Core.Contracts.Repositories;
using DotNetOrmComparison.Core.Entities;
using DotNetOrmComparison.Core.Shared.DTOs;
using DotNetOrmComparison.Core.Shared.InputModels;

namespace DotNetOrmComparison.Data.Dapper;

public class EmployeeDapperRepository : IEmployeeDapperRepository
{
    private readonly DapperDbContext _context;

    public EmployeeDapperRepository(DapperDbContext context)
    {
        _context = context;
    }

    public async Task<PagedListResult<EmployeeListResult>> GetAll(PaginationOptions pagination)
    {
        var (pageIndex, pageSize, skip, addTotalCount) = pagination;
        
        await using var dbConnection = await _context.CreateAndOpenConnectionAsync();

        // Get the total count of records
        int? totalCount = null;
        if (addTotalCount)
        {
            const string countQuery = "SELECT COUNT(*) FROM Employees";
            
            totalCount = await dbConnection.ExecuteScalarAsync<int>(countQuery);
        }

        const string sql = """
                           SELECT
                            e."Id", e."FirstName", e."LastName", e."Gender", e."Email", e."Salary",
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

        await dbConnection
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

    public async Task<EmployeeDetailResult?> GetById(Guid id)
    {
        const string sql = """
                           SELECT
                            e.*,
                            a."Street", a."City", a."State", a."ZipCode",
                            d."Id", d."Name",
                            p."Id", p."Name"
                           FROM (
                            SELECT e0.*
                            FROM "Employees" AS e0
                            WHERE e0."Id" = @Id
                           ) AS e
                           LEFT JOIN "Addresses" AS a ON e."Id" = a."EmployeeId"
                           LEFT JOIN "Departments" AS d ON e."DepartmentId" = d."Id"
                           LEFT JOIN (
                            SELECT p0."Id", p0."Name", ep."EmployeeId"
                               FROM "EmployeeProjects" AS ep
                               INNER JOIN "Projects" AS p0 ON ep."ProjectId" = p0."Id"
                           ) AS p ON e."Id" = p."EmployeeId"
                           """;

        EmployeeDetailResult? employeeDetail = null;
        
        await using var dbConnection = await _context.CreateAndOpenConnectionAsync();

        await dbConnection
            .QueryAsync<EmployeeDetailResult, AddressDto, KeyValueDto<Guid>, KeyValueDto<Guid>, EmployeeDetailResult>(
                sql,
                (employee, address, department, project) =>
                {
                    if (employeeDetail == null)
                    {
                        employeeDetail = employee;
                        employeeDetail.Address = address;
                        employeeDetail.Department = department;
                        employeeDetail.Projects = new List<KeyValueDto<Guid>>();
                    }

                    if (project != null)
                    {
                        employeeDetail.Projects.Add(project);
                    }

                    return employeeDetail;
                },
                new { Id = id },
                splitOn: "Street,Id,Id" // Split on Address.Street, DepartmentId, ProjectId
            );

        return employeeDetail;
    }

    public async Task<bool> Add(Employee item)
    {
        const string employeeSql = """
                                   INSERT INTO "Employees" (
                                   	"Id"
                                   	,"FirstName"
                                   	,"LastName"
                                   	,"Gender"
                                   	,"Email"
                                   	,"HireDate"
                                   	,"Salary"
                                   	,"IsActive"
                                   	,"HourlyRate"
                                   	,"MaritalStatus"
                                   	,"CreatedAt"
                                   	,"UpdatedAt"
                                   	,"DepartmentId"
                                   )
                                   VALUES (
                                   	@Id
                                   	,@FirstName
                                   	,@LastName
                                   	,@Gender
                                   	,@Email
                                   	,@HireDate
                                   	,@Salary
                                   	,@IsActive
                                   	,@HourlyRate
                                   	,@MaritalStatus
                                   	,@CreatedAt
                                   	,@UpdatedAt
                                   	,@DepartmentId
                                   )
                                   """;
        const string addressSql = """
                                  INSERT INTO "Addresses" (
                                  	"EmployeeId"
                                  	,"Street"
                                  	,"City"
                                  	,"State"
                                  	,"ZipCode"
                                  )
                                  VALUES (
                                  	@EmployeeId
                                  	,@Street
                                  	,@City
                                  	,@State
                                  	,@ZipCode
                                  )
                                  """;
        const string projectSql = """
                                  INSERT INTO "EmployeeProjects" ("EmployeeId", "ProjectId")
                                  VALUES (@EmployeeId, @ProjectId)
                                  """;

        await using var dbConnection = await _context.CreateAndOpenConnectionAsync();
        await using var transaction = await dbConnection.BeginTransactionAsync();
        
        var employeeRowsAffected = 0;
        var addressRowsAffected = 0;
        var projectRowsAffected = 0;
        
        try
        {
            employeeRowsAffected = await dbConnection.ExecuteAsync(employeeSql, item, transaction);
            addressRowsAffected = await dbConnection.ExecuteAsync(addressSql, item.Address, transaction);
            foreach (var ep in item.EmployeeProjects)
            {
                projectRowsAffected += await dbConnection.ExecuteAsync(projectSql, ep, transaction);
            }

            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
        }

        return employeeRowsAffected == 1 
               && addressRowsAffected == 1 
               && projectRowsAffected == item.EmployeeProjects.Count;
    }
}
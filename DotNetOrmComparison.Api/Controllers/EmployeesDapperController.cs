using DotNetOrmComparison.Core.Contracts.Services;
using DotNetOrmComparison.Core.Shared.InputModels;
using Microsoft.AspNetCore.Mvc;

namespace DotNetOrmComparison.Api.Controllers;

[ApiController]
[Route("api/dapper/employees")]
public class EmployeesDapperController: ControllerBase
{
    private readonly IEmployeeDapperService _employeeService;
    
    public EmployeesDapperController(IEmployeeDapperService employeeService)
    {
        _employeeService = employeeService;
    }
    
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] int? pageIndex, [FromQuery] int? pageSize, bool? addTotalCount)
    {
        var pagination = new PaginationOptions(pageIndex, pageSize, addTotalCount);
        var result = await _employeeService.GetAll(pagination);

        return StatusCode(StatusCodes.Status200OK, result);
    }
}
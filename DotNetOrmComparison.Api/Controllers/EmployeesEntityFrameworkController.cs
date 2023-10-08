using DotNetOrmComparison.Core.Contracts.Services;
using DotNetOrmComparison.Core.Shared.InputModels;
using Microsoft.AspNetCore.Mvc;

namespace DotNetOrmComparison.Api.Controllers;

[ApiController]
[Route("api/ef/employees")]
public class EmployeesEntityFrameworkController: ControllerBase
{
    private readonly IEmployeeEntityFrameworkService _employeeService;
    
    public EmployeesEntityFrameworkController(IEmployeeEntityFrameworkService employeeService)
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
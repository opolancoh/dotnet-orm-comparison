using DotNetOrmComparison.Core.Contracts.Services;
using DotNetOrmComparison.Core.Shared.InputModels;
using Microsoft.AspNetCore.Mvc;

namespace DotNetOrmComparison.Api.Controllers;

[ApiController]
[Route("api/dapper/employees")]
public class EmployeesDapperController: ControllerBase
{
    private readonly IEmployeeDapperService _service;
    
    public EmployeesDapperController(IEmployeeDapperService service)
    {
        _service = service;
    }
    
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] int? pageIndex, [FromQuery] int? pageSize, bool? addTotalCount)
    {
        var pagination = new PaginationOptions(pageIndex, pageSize, addTotalCount);
        var result = await _service.GetAll(pagination);

        return StatusCode(StatusCodes.Status200OK, result);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _service.GetById(id);

        return StatusCode(StatusCodes.Status200OK, result);
    }
}
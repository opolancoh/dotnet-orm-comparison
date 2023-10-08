using System.Text.Json.Serialization;

namespace DotNetOrmComparison.Core.Shared.DTOs;

public record ApplicationResult
{
    public required int Status { get; init; }
    
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Message { get; init; }
    
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public IList<ApplicationResultError>? Errors { get; init; }
    
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public PageInfo? PageInfo { get; init; }
    
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public object? Data { get; init; }
}

public record ApplicationResultError
{
    public required string Code { get; set; }
    public required string Description { get; set; }
}
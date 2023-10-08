using System.Text.Json.Serialization;

namespace DotNetOrmComparison.Core.Shared.DTOs;

public record PagedListResult<T>
{
    public IEnumerable<T> Data { get; set; }
    public PageInfo PageInfo { get; set; }
}

public record PageInfo
{
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
    
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? TotalCount { get; set; }
    // public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
}
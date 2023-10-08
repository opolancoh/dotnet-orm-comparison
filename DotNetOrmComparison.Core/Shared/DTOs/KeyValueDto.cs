namespace DotNetOrmComparison.Core.Shared.DTOs;

public record KeyValueDto<T>
{
    public T Id { get; set; }
    public string Name { get; set; }
}
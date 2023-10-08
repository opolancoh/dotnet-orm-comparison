namespace DotNetOrmComparison.Core.Shared.DTOs;

public record AddressShortDto 
{
    public string Street { get; init; }
    public string City { get; init; }
    public string State { get; init; }
}
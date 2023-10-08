namespace DotNetOrmComparison.Core.Shared.DTOs;

public record AddressDto 
{
    public string Street { get; init; }
    public string City { get; init; }
    public string State { get; init; }
    public string ZipCode { get; init; }
}
namespace DotNetOrmComparison.Core.Shared.InputModels;

public record PaginationOptions
{
    private const int MaxPageSize = 100;
    private const int DefaultPageSize = 10;
    private const int DefaultPageIndex = 1;

    public int PageIndex { get; init; }
    public int PageSize { get; init; }
    public int Skip { get; init; }
    public bool AddTotalCount { get; init; }

    public PaginationOptions(int? pageIndex, int? pageSize, bool? addTotalCount)
    {
        PageIndex = pageIndex switch
        {
            null or < 1 => DefaultPageIndex,
            _ => (int)pageIndex
        };

        PageSize = pageSize switch
        {
            null or < 1 => DefaultPageSize,
            > MaxPageSize => MaxPageSize,
            _ => (int)pageSize
        };

        Skip = (PageIndex - 1) * PageSize;

        AddTotalCount = addTotalCount switch
        {
            null => false,
            _ => (bool)addTotalCount
        };
    }

    public void Deconstruct(out int pageIndex, out int pageSize, out int skip, out bool addTotalCount)
    {
        pageIndex = PageIndex;
        pageSize = PageSize;
        skip = Skip;
        addTotalCount = AddTotalCount;
    }
}
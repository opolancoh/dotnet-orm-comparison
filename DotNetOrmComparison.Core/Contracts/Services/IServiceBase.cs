namespace DotNetOrmComparison.Core.Contracts.Services;

public interface IServiceBase<TResult, in TKey, in TCreateOrUpdate>
{
    Task<TResult> GetAll();
    Task<TResult> GetById(TKey id);
    Task<TResult> Create(TCreateOrUpdate item);
    Task<TResult> Update(TKey id, TCreateOrUpdate item);
    Task<TResult> Remove(TKey id);
}
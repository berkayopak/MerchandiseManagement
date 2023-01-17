namespace MerchandiseManagementApi.Repository;

public interface ISimpleCrudRepository<T>
{
    public Task<T> Add(T entity);
    public Task<T?> Find(int id);
    public Task<bool> Update(T entity);
    public Task<bool> Delete(T entity);
}
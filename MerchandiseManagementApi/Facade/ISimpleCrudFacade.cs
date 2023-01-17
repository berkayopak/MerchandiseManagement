namespace MerchandiseManagementApi.Facade;

public interface ISimpleCrudFacade<T>
{
    public Task<T> Add(T entity);
    public Task<T?> Find(int id);
    public Task<bool> Update(T entity);
    public Task<bool> Delete(int id);
}
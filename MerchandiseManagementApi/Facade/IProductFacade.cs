using MerchandiseManagementApi.Domain;

namespace MerchandiseManagementApi.Facade;

public interface IProductFacade : ISimpleCrudFacade<Product>
{
    public Task<IEnumerable<Product>> Search(string? keyword, (int Min, int Max) stockQuantity);
}
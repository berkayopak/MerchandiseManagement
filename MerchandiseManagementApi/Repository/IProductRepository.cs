using MerchandiseManagementApi.Domain;

namespace MerchandiseManagementApi.Repository;

public interface IProductRepository : ISimpleCrudRepository<Product>
{
    public Task<IEnumerable<Product>> Search(string? keyword, (int Min, int Max) stockQuantity);
}
using MerchandiseManagementApi.Common;
using MerchandiseManagementApi.Domain;
using MerchandiseManagementApi.Repository;

namespace MerchandiseManagementApi.Facade;

public class ProductFacade : IProductFacade
{
    private readonly IProductRepository _productRepository;
    private readonly ICategoryRepository _categoryRepository;

    public ProductFacade(IProductRepository productRepository, ICategoryRepository categoryRepository)
    {
        _productRepository = productRepository;
        _categoryRepository = categoryRepository;
    }

    public async Task<Product> Add(Product product)
    {
        var category = await _categoryRepository.Find(product.CategoryId);
        if (category == null)
            throw new CustomApplicationException("A category could not be found for the entered categoryId value.",
                StatusCodes.Status422UnprocessableEntity);

        if (product.StockQuantity < category.MinStockQuantity && product.Status == Status.Live)
            throw new CustomApplicationException(
                "Products whose stock amount is below the category min stock limit cannot be live.",
                StatusCodes.Status422UnprocessableEntity);

        return await _productRepository.Add(product);
    }

    public async Task<Product?> Find(int id) =>
        await _productRepository.Find(id);

    public async Task<bool> Update(Product product)
    {
        var oldProduct = await _productRepository.Find(product.Id);
        if (oldProduct?.Category == null)
            throw new CustomApplicationException("A product with the entered id value was not found.",
                StatusCodes.Status404NotFound);

        Category? category = null;

        if (product.CategoryId != oldProduct.CategoryId)
        {
            category = await _categoryRepository.Find(product.CategoryId);
            if (category == null)
                throw new CustomApplicationException("A category could not be found for the new categoryId value.",
                    StatusCodes.Status422UnprocessableEntity);
        }

        category ??= oldProduct.Category;

        if (product.StockQuantity < category.MinStockQuantity && product.Status == Status.Live)
            throw new CustomApplicationException(
                "Products whose stock amount is below the category min stock limit cannot be live.",
                StatusCodes.Status422UnprocessableEntity);

        var newProduct = new Product(oldProduct.Id, oldProduct.CreatedAt, DateTime.Now, oldProduct.Active,
            product.Title, product.Description, product.CategoryId, category, product.StockQuantity, product.Status);

        return await _productRepository.Update(newProduct);
    }

    public async Task<bool> Delete(int id)
    {
        var product = await _productRepository.Find(id);
        if(product == null)
            throw new CustomApplicationException("A product with the entered id value was not found.",
                StatusCodes.Status404NotFound);

        product.SetActive(false);
        product.SetUpdatedAt(DateTime.Now);

        return await _productRepository.Update(product);
    }

    //P.S: Under normal circumstances, pagination functionality should be added to the search function.
    //Since it is a poc project, I did not develop that part.
    public async Task<IEnumerable<Product>> Search(string? keyword, (int Min, int Max) stockQuantity) =>
        await _productRepository.Search(keyword, stockQuantity);
}
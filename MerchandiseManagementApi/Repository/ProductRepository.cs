using MerchandiseManagementApi.Domain;
using MerchandiseManagementApi.Dto.Source;
using Microsoft.EntityFrameworkCore;

namespace MerchandiseManagementApi.Repository;

public class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _context;

    public ProductRepository(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<Product> Add(Product product)
    {
        var result = (await _context.Products.AddAsync(new ProductDto(product))).Entity;
        await _context.SaveChangesAsync();

        return result.ToProduct();
    }

    public async Task<Product?> Find(int id) =>
        (await _context.Products
            .Include(p => p.Category)
            .FirstOrDefaultAsync(p => p.Id == id))
        ?.ToProduct();

    public async Task<bool> Update(Product product)
    {
        var productDto = _context.Products.FirstOrDefault(p => p.Id == product.Id);
        if (productDto == null)
            return false;

        productDto.Title = product.Title;
        productDto.Description = product.Description;
        productDto.CategoryId = product.CategoryId;
        productDto.StockQuantity = product.StockQuantity;
        productDto.Status = product.Status;
        productDto.UpdatedAt = product.UpdatedAt;
        productDto.Active = product.Active;

        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> Delete(Product product)
    {
        _context.Products.Remove(new ProductDto(product));
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<IEnumerable<Product>> Search(string? keyword, (int Min, int Max) stockQuantity)
    {
        return await _context.Products
            .Include(p => p.Category)
            .Where(x => string.IsNullOrWhiteSpace(keyword)
                        || x.Title.Contains(keyword)
                        || x.Description.Contains(keyword)
                        || x.Category.Title.Contains(keyword))
            .Where(p => (stockQuantity.Min <= 0 || stockQuantity.Min <= p.StockQuantity)
                        && (stockQuantity.Max <= 0 || stockQuantity.Max >= p.StockQuantity))
            .Select(p => p.ToProduct())
            .ToListAsync();
    }
}
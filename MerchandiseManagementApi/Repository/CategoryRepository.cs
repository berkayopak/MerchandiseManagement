using MerchandiseManagementApi.Domain;
using MerchandiseManagementApi.Dto.Source;

namespace MerchandiseManagementApi.Repository;

public class CategoryRepository : ICategoryRepository
{
    private readonly ApplicationDbContext _context;

    public CategoryRepository(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<Category> Add(Category category)
    {
        var result = (await _context.Categories.AddAsync(new CategoryDto(category))).Entity;
        await _context.SaveChangesAsync();

        return result.ToCategory();
    }

    public async Task<Category?> Find(int id)=>
        (await _context.Categories.FindAsync(id))?.ToCategory();
    
    public async Task<bool> Update(Category category)
    {
        var categoryDto = _context.Categories.FirstOrDefault(c => c.Id == category.Id);
        if (categoryDto == null)
            return false;

        categoryDto.Title = category.Title;
        categoryDto.Status = category.Status;
        categoryDto.MinStockQuantity = category.MinStockQuantity;
        categoryDto.UpdatedAt = category.UpdatedAt;
        categoryDto.Active = category.Active;

        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> Delete(Category category)
    {
        _context.Categories.Remove(new CategoryDto(category));
        return await _context.SaveChangesAsync() > 0;
    }
}
using MerchandiseManagementApi.Common;
using MerchandiseManagementApi.Domain;
using MerchandiseManagementApi.Repository;

namespace MerchandiseManagementApi.Facade;

public class CategoryFacade : ICategoryFacade
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryFacade(ICategoryRepository categoryRepository) =>
        _categoryRepository = categoryRepository;

    public async Task<Category> Add(Category category) =>
        await _categoryRepository.Add(category);

    public async Task<Category?> Find(int id) =>
        await _categoryRepository.Find(id);

    public async Task<bool> Update(Category category)
    {
        var oldCategory = await _categoryRepository.Find(category.Id);
        if (oldCategory == null)
            throw new CustomApplicationException("A category with the entered id value was not found.",
                StatusCodes.Status404NotFound);

        var newCategory = new Category(oldCategory.Id, oldCategory.CreatedAt, DateTime.Now, oldCategory.Active,
            category.Title, category.Status, category.MinStockQuantity);

        return await _categoryRepository.Update(newCategory);
    }

    public async Task<bool> Delete(int id)
    {
        var category = await _categoryRepository.Find(id);
        if (category == null)
            throw new CustomApplicationException("A category with the entered id value was not found.",
                StatusCodes.Status404NotFound);

        category.SetActive(false);
        category.SetUpdatedAt(DateTime.Now);

        return await _categoryRepository.Update(category);
    }
}
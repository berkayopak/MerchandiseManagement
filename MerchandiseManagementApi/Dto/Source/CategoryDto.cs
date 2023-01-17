using MerchandiseManagementApi.Domain;

namespace MerchandiseManagementApi.Dto.Source;

public class CategoryDto : AuditableBaseDto
{
    public string Title { get; set; }
    public Status Status { get; set; }
    public int MinStockQuantity { get; set; }
    public virtual ICollection<ProductDto> Products { get; private set; }

    private CategoryDto(){}

    public CategoryDto(Category category) : base(category.Id, category.CreatedAt, category.UpdatedAt, category.Active)
    {
        Title = category.Title;
        Status = category.Status;
        MinStockQuantity = category.MinStockQuantity;
    }

    public Category ToCategory() => new(Id, CreatedAt, UpdatedAt, Active, Title, Status, MinStockQuantity);
}
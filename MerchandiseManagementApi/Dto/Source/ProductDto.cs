using MerchandiseManagementApi.Domain;

namespace MerchandiseManagementApi.Dto.Source;

public class ProductDto : AuditableBaseDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public int CategoryId { get; set; }
    public int StockQuantity { get; set; }
    public Status Status { get; set; }
    public virtual CategoryDto Category { get; private set; }

    private ProductDto(){}

    public ProductDto(Product product) : base(product.Id, product.CreatedAt, product.UpdatedAt, product.Active)
    {
        Title = product.Title;
        Description = product.Description;
        CategoryId = product.CategoryId;
        StockQuantity = product.StockQuantity;
        Status = product.Status;
    }

    public Product ToProduct() => new(Id, CreatedAt, UpdatedAt, Active, Title, Description, CategoryId,
        Category.ToCategory(), StockQuantity, Status);
}
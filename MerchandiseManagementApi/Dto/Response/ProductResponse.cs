using MerchandiseManagementApi.Domain;

namespace MerchandiseManagementApi.Dto.Response;

public class ProductResponse : AuditableResponse
{
    public string Title { get; }
    public string Description { get; }
    public CategoryResponse? Category { get; }
    public int StockQuantity { get; }
    public Status Status { get; }

    public ProductResponse(Product product) : base(product.Id, product.CreatedAt, product.UpdatedAt, product.Active)
    {
        Title = product.Title;
        Description = product.Description;
        Category = product.Category != null
            ? new CategoryResponse(product.Category)
            : null;
        StockQuantity = product.StockQuantity;
        Status = product.Status;
    }
}
using MerchandiseManagementApi.Domain;

namespace MerchandiseManagementApi.Dto.Response;

public class CategoryResponse : AuditableBase
{
    public string Title { get; }
    public Status Status { get; }
    public int MinStockQuantity { get; }

    public CategoryResponse(Category category) : base(category.Id, category.CreatedAt, category.UpdatedAt, category.Active)
    {
        Title = category.Title;
        Status = category.Status;
        MinStockQuantity = category.MinStockQuantity;
    }
}
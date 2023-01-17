namespace MerchandiseManagementApi.Domain;

public class Category : AuditableBase
{
    public string Title { get; }
    public Status Status { get; }
    public int MinStockQuantity { get; }

    public Category(
        int id,
        DateTime createdAt,
        DateTime updatedAt,
        bool active,
        string title,
        Status status,
        int minStockQuantity) : base(id, createdAt, updatedAt, active)
    {
        Title = title;
        Status = status;
        MinStockQuantity=minStockQuantity;
    }
}
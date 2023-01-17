namespace MerchandiseManagementApi.Domain;

public class Product : AuditableBase
{
    public string Title { get; }
    public string Description { get; }
    public int CategoryId { get; }
    public Category? Category { get; }
    public int StockQuantity { get; }
    public Status Status { get; }

    public Product(
        int id,
        DateTime createdAt,
        DateTime updatedAt,
        bool active,
        string title,
        string description,
        int categoryId,
        Category? category,
        int stockQuantity,
        Status status) : base(id, createdAt, updatedAt, active)
    {
        Title = title;
        Description = description;
        CategoryId = categoryId;
        Category = category;
        StockQuantity = stockQuantity;
        Status = status;
    }
}

public enum Status
{
    OnHold = 0,
    Live = 10
}
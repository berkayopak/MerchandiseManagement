namespace MerchandiseManagementApi.Domain;

public class AuditableBase
{
    public int Id { get; }
    public DateTime CreatedAt { get; }
    public DateTime UpdatedAt { get; private set; }
    public bool Active { get; private set; }

    public void SetActive(bool active) => Active = active;
    public void SetUpdatedAt(DateTime updatedAt) => UpdatedAt = updatedAt;

    private AuditableBase(){}

    public AuditableBase(int id, DateTime createdAt, DateTime updatedAt, bool active)
    {
        Id = id;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
        Active = active;
    }
}
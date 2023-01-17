namespace MerchandiseManagementApi.Dto.Response;

public class AuditableResponse
{
    public int Id { get; }
    public DateTime CreatedAt { get; }
    public DateTime UpdatedAt { get; }
    public bool Active { get; }

    public AuditableResponse(int id, DateTime createdAt, DateTime updatedAt, bool active)
    {
        Id = id;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
        Active = active;
    }
}
namespace MerchandiseManagementApi.Dto.Source;

public class AuditableBaseDto
{
    public int Id { get; private set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public bool Active { get; set; }

    protected AuditableBaseDto(){}

    public AuditableBaseDto(int id, DateTime createdAt, DateTime updatedAt, bool active)
    {
        Id = id;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
        Active = active;
    }
}
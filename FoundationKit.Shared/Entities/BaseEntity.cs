namespace FoundationKit.Shared.Entities;
public class BaseEntity<TId>
{
    public TId Id { get; set; } = default!;
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public DateTime? LastModifiedDate { get; set; }
}


public class BaseEntity : BaseEntity<Guid>
{
    public new Guid Id { get; set; } = Guid.NewGuid();
}
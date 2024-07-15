namespace ProtEquity.Domain.Entities;
public class SubCategories : BaseAuditableEntity
{
    public SubCategories()
    {
    }
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int CategoryId { get; set; }
    public virtual Categories Categories { get; set; } = null!;
}

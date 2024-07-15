namespace CleanArchitecture.Domain.Entities;
public class Categories : BaseAuditableEntity
{
    public Categories()
    {
        SubCategories = new HashSet<SubCategories>();
    }
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public virtual ICollection<SubCategories> SubCategories { get; set; }
}

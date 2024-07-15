using ProtEquity.Application.Common.Mappings;
using ProtEquity.Domain.Entities;

namespace ProtEquity.Application.SubCategory.Queries.GetAllSubCategories;
public class SubCategoriesDto : IMapFrom<SubCategories>
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public CategoryDto Categories { get; set; } = null!;
}
public class CategoryDto : IMapFrom<Categories>
{
    public int Id { get; set; }
    public string? Name { get; set; }
}

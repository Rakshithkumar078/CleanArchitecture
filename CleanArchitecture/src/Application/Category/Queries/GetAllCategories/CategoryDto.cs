using CleanArchitecture.Application.Common.Mappings;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Category.Queries.GetAllCategories;
public class CategoryDto : IMapFrom<Categories>
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public List<SubCategoryDto>? SubCategories { get; set; }
}

public class SubCategoryDto : IMapFrom<SubCategories>
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int CategoryId { get; set; }
}

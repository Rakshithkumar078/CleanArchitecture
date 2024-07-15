using CleanArchitecture.Application.Common.Mappings;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Category.Queries.GetCategoryById;
public class GetCategoryByIdDto : IMapFrom<Categories>
{
    public int Id { get; set; }
    public string? Name { get; set; }
}

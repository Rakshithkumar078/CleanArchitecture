using ProtEquity.Application.Common.Mappings;
using ProtEquity.Domain.Entities;

namespace ProtEquity.Application.SubCategory.Queries.GetSubCategoryById;
public class GetSubCategoryByIdDto : IMapFrom<SubCategories>
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int CategoryId { get; set; }
}

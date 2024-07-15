using ProtEquity.Application.Common.Mappings;
using ProtEquity.Domain.Entities;

namespace ProtEquity.Application.Category.Queries.GetCategoryById;
public class GetCategoryByIdDto : IMapFrom<Categories>
{
    public int Id { get; set; }
    public string? Name { get; set; }
}

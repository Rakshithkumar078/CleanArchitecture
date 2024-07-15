using CleanArchitecture.Application.Common.Interfaces;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Application.Category.Queries.GetAllCategories;
public class GetAllCategoriesQuery : IRequest<List<CategoryDto>>
{
}

public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, List<CategoryDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly ILogger<GetAllCategoriesQueryHandler> _logger;

    public GetAllCategoriesQueryHandler(IApplicationDbContext context, IMapper mapper, ILogger<GetAllCategoriesQueryHandler> logger)
    {
        _context = context;
        _mapper = mapper;
        _logger = logger;
    }

    /// <summary>
    /// To Get All Categories
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<List<CategoryDto>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("GetAllCategories(): entry");
        try
        {
            var categories = await _context.Categories
                .Include(c => c.SubCategories)
                .OrderByDescending(c => c.Id)
                .ProjectTo<CategoryDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return categories;
        }
        catch (Exception ex)
        {
            _logger.LogError($"GetAllCategories(): Error while getting categories {ex}");
            return new List<CategoryDto>();
        }
    }
}

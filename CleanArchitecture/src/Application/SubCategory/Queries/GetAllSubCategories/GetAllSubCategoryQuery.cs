using CleanArchitecture.Application.Common.Interfaces;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Application.SubCategory.Queries.GetAllSubCategories;
public class GetAllSubCategoryQuery : IRequest<List<SubCategoriesDto>>;
public class GetAllSubCategoryQueryHandler : IRequestHandler<GetAllSubCategoryQuery, List<SubCategoriesDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly ILogger<GetAllSubCategoryQueryHandler> _logger;

    public GetAllSubCategoryQueryHandler(IApplicationDbContext context, IMapper mapper, ILogger<GetAllSubCategoryQueryHandler> logger)
    {
        _context = context;
        _mapper = mapper;
        _logger = logger;
    }
    /// <summary>
    /// To get the list of SubCategories
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<List<SubCategoriesDto>> Handle(GetAllSubCategoryQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("GetAllSubCategoryQuery():entry");
        try
        {
            return await _context.SubCategories.Include(c => c.Categories).OrderByDescending(x => x.Id)
                                    .ProjectTo<SubCategoriesDto>(_mapper.ConfigurationProvider).ToListAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError("GetAllSubCategoryQuery(): Error while get all SubCategories" + ex);
            return new List<SubCategoriesDto>();
        }
    }
}

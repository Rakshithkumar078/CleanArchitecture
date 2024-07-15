using CleanArchitecture.Application.Common.Interfaces;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Application.SubCategory.Queries.GetSubCategoryById;
public class GetSubCategoryByIdQuery : IRequest<GetSubCategoryByIdDto>
{
    public int Id { get; set; }
}
public class GetSubCategoryByIdQueryHandler : IRequestHandler<GetSubCategoryByIdQuery, GetSubCategoryByIdDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly ILogger<GetSubCategoryByIdQueryHandler> _logger;

    public GetSubCategoryByIdQueryHandler(IApplicationDbContext context, IMapper mapper, ILogger<GetSubCategoryByIdQueryHandler> logger)
    {
        _context = context;
        _mapper = mapper;
        _logger = logger;
    }

    /// <summary>
    /// To Get sub-category by id
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<GetSubCategoryByIdDto> Handle(GetSubCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("GetSubCategoryByIdQuery(): entry");
        try
        {
            var subCategory = await _context.SubCategories.FindAsync(request.Id);
            if (subCategory == null)
            {
                throw new NotFoundException(nameof(SubCategory), request.Id.ToString());
            }
            return _mapper.Map<GetSubCategoryByIdDto>(subCategory);
        }
        catch (Exception ex)
        {
            _logger.LogError($"GetSubCategoryByIdQuery(): Error while getting sub-category by Id {request.Id} {ex}");
            return new GetSubCategoryByIdDto();
        }
    }
}

using ProtEquity.Application.Common.Interfaces;
using Microsoft.Extensions.Logging;

namespace ProtEquity.Application.Category.Queries.GetCategoryById;
public class GetCategoryByIdQuery : IRequest<GetCategoryByIdDto>
{
    public int Id { get; set; }
}

public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, GetCategoryByIdDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly ILogger<GetCategoryByIdQueryHandler> _logger;

    public GetCategoryByIdQueryHandler(IApplicationDbContext context, IMapper mapper, ILogger<GetCategoryByIdQueryHandler> logger)
    {
        _context = context;
        _mapper = mapper;
        _logger = logger;
    }

    /// <summary>
    /// To Get Category by id
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<GetCategoryByIdDto> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("GetCategoryByIdQuery(): entry");
        try
        {
            var category = await _context.Categories.FindAsync(request.Id);
            if (category == null)
            {
                throw new NotFoundException(nameof(Category), request.Id.ToString());
            }
            return _mapper.Map<GetCategoryByIdDto>(category);
        }
        catch (Exception ex)
        {
            _logger.LogError($"GetCategoryByIdQuery(): Error while getting category by Id {request.Id} {ex}");
            return new GetCategoryByIdDto();
        }
    }
}

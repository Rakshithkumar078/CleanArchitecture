using ProtEquity.Application.Common.Interfaces;
using ProtEquity.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace ProtEquity.Application.SubCategory.Commands.CreateSubCategory;
public class CreateSubCategoryCommand : IRequest<int>
{
    public string Name { get; set; } = null!;
    public int CategoryId { get; set; }
}
public class CreateSubCategoryCommandHandler : IRequestHandler<CreateSubCategoryCommand, int>
{
    private readonly IApplicationDbContext _context;
    private readonly ILogger<CreateSubCategoryCommandHandler> _logger;

    public CreateSubCategoryCommandHandler(IApplicationDbContext context, ILogger<CreateSubCategoryCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }
    /// <summary>
    /// To create a SubCategory
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<int> Handle(CreateSubCategoryCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("CreateSubCategoryCommand():entry");
        try
        {
            SubCategories SubCategory = BuildModel(command);
            await _context.SubCategories.AddAsync(SubCategory);
            await _context.SaveChangesAsync(cancellationToken);
            return SubCategory.Id;
        }
        catch (Exception ex)
        {
            _logger.LogError($"CreateSubCategoryCommand(): Error while creating SubCategory {ex}");
            return int.MinValue;
        }
    }

    /// <summary>
    /// To build Subcategory entity
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    private SubCategories BuildModel(CreateSubCategoryCommand command)
    {
        return new SubCategories
        {
            Name = command.Name,
            CategoryId = command.CategoryId,
        };
    }
}

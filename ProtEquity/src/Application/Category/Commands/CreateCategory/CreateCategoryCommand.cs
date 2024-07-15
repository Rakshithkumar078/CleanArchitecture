using ProtEquity.Application.Common.Interfaces;
using ProtEquity.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace ProtEquity.Application.Category.Commands.CreateCategory;
public class CreateCategoryCommand : IRequest<int>
{
    public string Name { get; set; } = null!;
}
public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, int>
{
    private readonly IApplicationDbContext _context;
    private readonly ILogger<CreateCategoryCommandHandler> _logger;

    public CreateCategoryCommandHandler(IApplicationDbContext context, ILogger<CreateCategoryCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <summary>
    /// Create Category
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<int> Handle(CreateCategoryCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("CreateCategoryCommand():entry");
        try
        {
            Categories createCategory = BuildModel(command);
            await _context.Categories.AddAsync(createCategory);
            await _context.SaveChangesAsync(cancellationToken);
            return createCategory.Id;
        }
        catch (Exception ex)
        {
            _logger.LogError($"CreateCategoryCommand(): Error while creating Category {ex}");
            return 0;
        }
    }

    /// <summary>
    /// To build category entity
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    private Categories BuildModel(CreateCategoryCommand command)
    {
        return new Categories
        {
            Name = command.Name
        };
    }
}

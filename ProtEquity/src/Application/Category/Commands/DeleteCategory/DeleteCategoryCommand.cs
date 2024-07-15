using ProtEquity.Application.Common.Interfaces;
using Microsoft.Extensions.Logging;

namespace ProtEquity.Application.Category.Commands.DeleteCategory;
public class DeleteCategoryCommand : IRequest<bool>
{
    public int Id { get; set; }
}

public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, bool>
{
    private readonly IApplicationDbContext _context;
    private readonly ILogger<DeleteCategoryCommandHandler> _logger;

    public DeleteCategoryCommandHandler(IApplicationDbContext context, ILogger<DeleteCategoryCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <summary>
    /// Delete Category
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="NotFoundException"></exception>
    public async Task<bool> Handle(DeleteCategoryCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("DeleteCategoryCommandHandler(): Entry");
        try
        {
            var category = await _context.Categories.Include(x => x.SubCategories)
                                    .FirstOrDefaultAsync(c => c.Id == command.Id, cancellationToken);
            if (category == null)
            {
                throw new NotFoundException(nameof(Category), command.Id.ToString());
            }
            category.IsDeleted = true;
            category?.SubCategories.ToList().ForEach(x => { x.IsDeleted = true; });
            _context.Categories.Update(category);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError($"DeleteCategoryCommand(): Error while deleting Category {command.Id}" + ex);
            return false;
        }
    }
}

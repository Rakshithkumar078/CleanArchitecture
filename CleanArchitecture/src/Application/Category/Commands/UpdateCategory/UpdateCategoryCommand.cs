using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Application.Category.Commands.UpdateCategory;
public class UpdateCategoryCommand : IRequest<bool>
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
}
public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, bool>
{
    private readonly IApplicationDbContext _context;
    private readonly ILogger<UpdateCategoryCommandHandler> _logger;

    public UpdateCategoryCommandHandler(IApplicationDbContext context, ILogger<UpdateCategoryCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <summary>
    /// Update Category
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool> Handle(UpdateCategoryCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("UpdateCategoryCommand():entry");
        try
        {
            var category = await _context.Categories.FindAsync(command.Id);
            if (category == null)
            {
                throw new NotFoundException(nameof(Categories), command.Name);
            }
            category.Name = command.Name;
            _context.Categories.Update(category);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError($"UpdateCategoryCommand(): Error while updating category {command.Id} ", ex);
            return false;
        }
    }
}

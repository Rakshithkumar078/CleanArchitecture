using CleanArchitecture.Application.Common.Interfaces;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Application.SubCategory.Commands.UpdateSubCategory;
public class UpdateSubCategoryCommand : IRequest<bool>
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int CategoryId { get; set; }
}
public class UpdateSubCategoryCommandHandler : IRequestHandler<UpdateSubCategoryCommand, bool>
{
    private readonly IApplicationDbContext _context;
    private readonly ILogger<UpdateSubCategoryCommandHandler> _logger;

    public UpdateSubCategoryCommandHandler(IApplicationDbContext context, ILogger<UpdateSubCategoryCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }
    /// <summary>
    /// To update SubCategory
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool> Handle(UpdateSubCategoryCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("UpdateSubCategoryCommand():entry");
        try
        {
            var subCategory = await _context.SubCategories.FindAsync(command.Id);
            if (subCategory == null)
            {
                throw new NotFoundException(nameof(SubCategory), command.Id.ToString());
            }
            subCategory.Name = command.Name;
            subCategory.CategoryId = command.CategoryId;
            _context.SubCategories.Update(subCategory);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError($"UpdateSubCategoryCommand(): Error while creating SubCategory {ex}");
            return false;
        }
    }
}

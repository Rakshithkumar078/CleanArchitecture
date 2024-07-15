using ProtEquity.Application.Common.Interfaces;
using Microsoft.Extensions.Logging;

namespace ProtEquity.Application.SubCategory.Commands.DeleteSubCategory;
public class DeleteSubCategoryCommand : IRequest<bool>
{
    public int Id { get; set; }
}

public class DeleteSubCategoryCommandHandler : IRequestHandler<DeleteSubCategoryCommand, bool>
{
    private readonly IApplicationDbContext _context;
    private readonly ILogger<DeleteSubCategoryCommandHandler> _logger;

    public DeleteSubCategoryCommandHandler(IApplicationDbContext context, ILogger<DeleteSubCategoryCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <summary>
    /// To delete Subcategory based on id 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="NotFoundException"></exception>
    public async Task<bool> Handle(DeleteSubCategoryCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("DeleteSubCategoryCommand():entry");
        try
        {
            var subCategory = await _context.SubCategories.FindAsync(request.Id);
            if (subCategory == null)
            {
                throw new NotFoundException(nameof(SubCategory), request.Id.ToString());
            }

            subCategory.IsDeleted = true;
            _context.SubCategories.Update(subCategory);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError($"DeleteSubCategoryCommand(): Error while deleting the Subcategory Id {request.Id}", ex);
            return false;
        }
    }
}

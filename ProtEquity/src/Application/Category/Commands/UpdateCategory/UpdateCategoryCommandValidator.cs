using ProtEquity.Application.Common.Constants;
using ProtEquity.Application.Common.Interfaces;

namespace ProtEquity.Application.Category.Commands.UpdateCategory;
public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
{
    private readonly IApplicationDbContext _context;
    public UpdateCategoryCommandValidator(IApplicationDbContext context)
    {
        _context = context;
        RuleFor(m => m.Name)
            .MaximumLength(50)
                .WithMessage(AppConstants.Category_NameLength_ErrorMessage)
            .MustAsync(BeUniqueName)
                .WithMessage(AppConstants.Category_NameExist_ErrorMessage);
    }

    /// <summary>
    /// To check the unique name of category while updating
    /// </summary>
    /// <param name="command"></param>
    /// <param name="name"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool> BeUniqueName(UpdateCategoryCommand command, string? name, CancellationToken cancellationToken)
    {
        var categoryName = await _context.Categories.Where(x => x.Id != command.Id).Select(g => g.Name).ToListAsync();
        return !categoryName.Any(existingName => string.Equals(existingName, name, StringComparison.OrdinalIgnoreCase));
    }
}

using CleanArchitecture.Application.Common.Constants;
using CleanArchitecture.Application.Common.Interfaces;

namespace CleanArchitecture.Application.Category.Commands.CreateCategory;
public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
{
    private readonly IApplicationDbContext _context;
    public CreateCategoryCommandValidator(IApplicationDbContext context)
    {
        _context = context;
        RuleFor(m => m.Name)
            .MaximumLength(50)
            .WithMessage(AppConstants.Category_NameLength_ErrorMessage)
            .MustAsync(BeUniqueName)
            .WithMessage(AppConstants.Category_NameExist_ErrorMessage);
    }

    /// <summary>
    /// To check the unique name of category
    /// </summary>
    /// <param name="name"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool> BeUniqueName(string name, CancellationToken cancellationToken)
    {
        var categoryName = await _context.Categories.Select(g => g.Name).ToListAsync();
        return !categoryName.Any(existingName => string.Equals(existingName, name, StringComparison.OrdinalIgnoreCase));
    }
}

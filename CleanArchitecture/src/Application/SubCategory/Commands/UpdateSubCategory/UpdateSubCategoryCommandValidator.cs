using CleanArchitecture.Application.Common.Constants;
using CleanArchitecture.Application.Common.Interfaces;

namespace CleanArchitecture.Application.SubCategory.Commands.UpdateSubCategory;
public class UpdateSubCategoryCommandValidator : AbstractValidator<UpdateSubCategoryCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateSubCategoryCommandValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(v => v.Name)
              .MaximumLength(50)
              .WithMessage(AppConstants.Subcategory_NameLength_ErrorMessage)
              .MustAsync(BeUniqueName)
              .WithMessage(AppConstants.SubCategory_Name_Already_Exists);
    }

    /// <summary>
    /// To check for unique names of the SubCategory in the SubCategories table
    /// </summary>
    /// <param name="name"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool> BeUniqueName(UpdateSubCategoryCommand model, string name, CancellationToken cancellationToken)
    {
        var subCategoryNameList = await _context.SubCategories.Where(x => x.Id != model.Id && x.CategoryId == model.CategoryId).Select(g => g.Name).ToListAsync();
        return !subCategoryNameList.Any(existingName => string.Equals(existingName, name, StringComparison.OrdinalIgnoreCase));
    }
}

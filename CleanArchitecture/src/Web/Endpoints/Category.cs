using CleanArchitecture.Application.Category.Commands.CreateCategory;
using CleanArchitecture.Application.Category.Commands.DeleteCategory;
using CleanArchitecture.Application.Category.Commands.UpdateCategory;
using CleanArchitecture.Application.Category.Queries.GetAllCategories;
using CleanArchitecture.Application.Category.Queries.GetCategoryById;

namespace CleanArchitecture.Web.Endpoints;
public class Category : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .MapPost(CreateCategory, "CreateCategory")
            .MapGet(GetAllCategories, "GetAllCategories")
            .MapPut(UpdateCategory, "UpdateCategory")
            .MapDelete(DeleteCategory, "DeleteCategory/{id}")
            .MapGet(GetCategoryById, "GetCategoryById/{id}");
    }

    /// <summary>
    /// Create Category
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    public async Task<int> CreateCategory(ISender sender, CreateCategoryCommand command)
    {
        return await sender.Send(command);
    }

    /// <summary>
    /// Update Category
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    public async Task<bool> UpdateCategory(ISender sender, UpdateCategoryCommand command)
    {
        return await sender.Send(command);
    }

    /// <summary>
    /// To Get all Categories
    /// </summary>
    /// <param name="sender"></param>
    /// <returns></returns>
    public async Task<List<CategoryDto>> GetAllCategories(ISender sender)
    {
        return await sender.Send(new GetAllCategoriesQuery());
    }

    /// <summary>
    /// Delete Category
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="command"></param>
    /// <returns></returns>
    public async Task<bool> DeleteCategory(ISender sender, int id)
    {
        return await sender.Send(new DeleteCategoryCommand { Id = id });
    }

    /// <summary>
    /// To Get Category by id
    /// </summary>
    /// <param name="sender"></param>
    /// <returns></returns>
    public async Task<GetCategoryByIdDto> GetCategoryById(ISender sender, int id)
    {
        return await sender.Send(new GetCategoryByIdQuery { Id = id });
    }
}

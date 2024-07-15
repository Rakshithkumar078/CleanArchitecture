using CleanArchitecture.Application.SubCategory.Commands.CreateSubCategory;
using CleanArchitecture.Application.SubCategory.Commands.DeleteSubCategory;
using CleanArchitecture.Application.SubCategory.Commands.UpdateSubCategory;
using CleanArchitecture.Application.SubCategory.Queries.GetAllSubCategories;
using CleanArchitecture.Application.SubCategory.Queries.GetSubCategoryById;

namespace CleanArchitecture.Web.Endpoints;
public class SubCategory : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
           .MapPost(CreateSubCategory, "CreateSubCategory")
           .MapGet(GetAllSubCategories, "GetAllSubCategories")
           .MapPut(UpdateSubCategory, "UpdateSubCategory")
           .MapDelete(DeleteSubCategory, "DeleteSubCategory/{id}")
           .MapGet(GetSubCategoryById, "GetSubCategoryById/{id}");
    }

    /// <summary>
    /// To create a SubCategory
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="CreateSubCategory"></param>
    /// <returns></returns>
    public async Task<int> CreateSubCategory(ISender sender, CreateSubCategoryCommand command)
    {
        return await sender.Send(command);
    }

    /// <summary>
    /// To get the list of SubCategories
    /// </summary>
    /// <param name="sender"></param>
    /// <returns></returns>
    public async Task<List<SubCategoriesDto>> GetAllSubCategories(ISender sender)
    {
        return await sender.Send(new GetAllSubCategoryQuery());
    }

    /// <summary>
    /// To Update a SubCategory
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="UpdateSubCategory"></param>
    /// <returns></returns>
    public async Task<bool> UpdateSubCategory(ISender sender, UpdateSubCategoryCommand command)
    {
        return await sender.Send(command);
    }

    /// <summary>
    /// To Delete a SubCategory
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="DeleteSubCategory"></param>
    /// <returns></returns>
    public async Task<bool> DeleteSubCategory(ISender sender, int id)
    {
        return await sender.Send(new DeleteSubCategoryCommand { Id = id });
    }

    /// <summary>
    /// To Get sub-category by id
    /// </summary>
    /// <param name="sender"></param>
    /// <returns></returns>
    public async Task<GetSubCategoryByIdDto> GetSubCategoryById(ISender sender, int id)
    {
        return await sender.Send(new GetSubCategoryByIdQuery { Id = id });
    }
}

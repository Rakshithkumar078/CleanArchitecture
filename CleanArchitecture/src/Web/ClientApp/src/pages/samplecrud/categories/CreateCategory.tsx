import { Link, useNavigate } from "react-router-dom";
import { useForm } from "react-hook-form";
import CategoryService from "../shared/services/CategoryService";
import SweetAlertService from "../../../services/SweetAlertService";
import { Category_Regex, errorMessages, getMaxLengthErrorMessage, getRequiredErrorMessage, max_limit_category } from "../shared/constants/AppConstant";

const CreateCategory = () => {
    const navigate = useNavigate();
    const { register, handleSubmit, reset, formState: { errors }, } = useForm();

    //method to submit the form
    const onSubmit = async (data: any) => {
        const postData = { name: data.category.trim() }; // Trim the category name
        try {
            const response = await CategoryService.postCategories(postData);
            if (response.data) {
                reset();
                await SweetAlertService.success(
                    "Success",
                    "Category created successfully"
                );
                navigate("/category");
            }
        } catch (error: any) {
            if (error?.response?.data?.errors?.Name[0]) {
                SweetAlertService.error("Error", error.response.data.errors.Name[0]);
            } else {
                SweetAlertService.error("Error", "Something went wrong while creating category");
            }
        }
    };

    return (
        <>
            <div className="panel-body">
                <div className="header-panel d-flex justify-content-between">
                    <h1>Create Category</h1>
                    <Link className="fs-5 fw-bold text-blue-800 align-self-center"
                        type="button"
                        to="/category">
                        Back
                    </Link>
                </div>
                <form onSubmit={handleSubmit(onSubmit)}>
                    <div className="mb-15px">
                        <div className="card border-2 p-4">
                            <div className="row mt-3">
                                <div className="col-md-6">
                                    <label className="mb-2">
                                        <b>Create Category</b>
                                    </label>
                                    <input
                                        type="text"
                                        id="description"
                                        className="form-control"
                                        {...register("category", {
                                            required: getRequiredErrorMessage("Category"),
                                            pattern: {
                                                value: Category_Regex,
                                                message: errorMessages?.pattern,
                                            },
                                            maxLength: {
                                                value: max_limit_category,
                                                message: getMaxLengthErrorMessage("Category", max_limit_category),
                                            },
                                        })}
                                    />
                                    {errors.category && (
                                        <p className="text-danger">
                                            {errors.category.message?.toString()}
                                        </p>
                                    )}
                                </div>
                            </div>
                            <div className="mt-3">
                                <button className="fs-15px fw-bold btn btn-dark bg-blue-800">
                                    Create Category
                                </button>
                                <button className="fw-bold btn btn-secondary mx-2"
                                    onClick={() => navigate("/category")}>
                                    Discard
                                </button>
                            </div>
                        </div>
                    </div>
                </form>
            </div>
        </>
    );
};

export default CreateCategory;
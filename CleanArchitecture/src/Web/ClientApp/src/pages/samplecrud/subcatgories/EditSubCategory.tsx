import { useEffect, useState } from "react";
import { useForm } from "react-hook-form";
import { useNavigate, useParams } from "react-router";
import SubCategoryService from "../shared/services/SubCategoryService";
import SweetAlertService from "../../../services/SweetAlertService";
import { Link } from "react-router-dom";
import { Category_Regex, errorMessages, getMaxLengthErrorMessage, getRequiredErrorMessage, max_limit_Subcategory } from "../shared/constants/AppConstant";
import CategoryService from "../shared/services/CategoryService";
import { Category } from "../categories/models/Category";

const EditSubCategory = () => {
    const navigate = useNavigate();
    const { id } = useParams();
    const { register, handleSubmit, setValue, reset, formState: { errors }, } = useForm();
    const [categories, setCategories] = useState<Category[]>([]);

    //method to get categories through API
    const getAllCategories = async () => {
        try {
            const response = await CategoryService.getAllCategories();
            setCategories(response.data);
        } catch (error) {
            SweetAlertService.error(
                "Error",
                "Something went wrong while fetching categories"
            );
        }
    };

    //method to get categories through API
    const getSubCategoryById = async () => {
        try {
            const response = await SubCategoryService.getSubCategoryById(Number(atob(id ?? '')));
            setValue('subcategory', response?.data?.name);
            setTimeout(() => {
                setValue('categoryId', response?.data?.categoryId);
            }, 25);
        } catch (error) {
            SweetAlertService.error(
                "Error",
                `Something went wrong while getting sub-category details`
            );
        }
    };

    //method to submit the category
    const onSubmit = async (data: any) => {
        const postData = {
            id: Number(atob(id ?? '')),
            name: data.subcategory.trim(),
            categoryId: data.categoryId
        };
        try {
            const response = await SubCategoryService.updateSubCategory(postData);
            if (response.data) {
                reset();
                await SweetAlertService.success(
                    "Success",
                    "SubCategory updated successfully"
                );
                navigate("/subCategory");
            }
        } catch (error: any) {
            if (error?.response?.data?.errors?.Name[0]) {
                SweetAlertService.error("Error", error.response.data.errors.Name[0]);
            } else {
                SweetAlertService.error("Error", "Something went wrong while creating sub-category");
            }
        }
    };

    useEffect(() => {
        getAllCategories();
        getSubCategoryById();
    }, []);

    return (
        <>
            <div className="panel-body">
                <div className="header-panel d-flex justify-content-between">
                    <h1>Edit SubCategory</h1>
                    <Link className="fs-5 fw-bold text-blue-800 align-self-center"
                        type="button"
                        to="/subCategory">
                        Back
                    </Link>
                </div>
                <form onSubmit={handleSubmit(onSubmit)}>
                    <div className="mb-15px">
                        <div className="card border-2 p-4">
                            <div className="row mt-3">
                                <div className="col-md-6">
                                    <label className="mb-2">
                                        <b>Edit SubCategory</b>
                                    </label>
                                    <input
                                        type="text"
                                        id="description"
                                        className="form-control"
                                        {...register("subcategory", {
                                            required: getRequiredErrorMessage("Subcategory"),
                                            pattern: {
                                                value: Category_Regex,
                                                message: errorMessages?.pattern,
                                            },
                                            maxLength: {
                                                value: max_limit_Subcategory,
                                                message: getMaxLengthErrorMessage("Subcategory", max_limit_Subcategory),
                                            },
                                        })}
                                    />
                                    {errors.subcategory && (
                                        <p className="text-danger">
                                            {errors.subcategory.message?.toString()}
                                        </p>
                                    )}
                                </div>
                            </div>
                            <div className="row mt-3">
                                <div className="col-md-6">
                                    <label className="mb-2">
                                        <b>Choose Category</b>
                                    </label>
                                    <select
                                        className="form-select"
                                        {...register("categoryId", {
                                            required: getRequiredErrorMessage("Category"),
                                        })}
                                    >
                                        <option value="" className="d-none">
                                            Choose Category
                                        </option>
                                        {categories.map((item) => (
                                            <option key={item.id} value={item.id}>
                                                {item.name}
                                            </option>
                                        ))}
                                    </select>
                                    {errors.categoryId && (
                                        <p className="text-danger">
                                            {errors.categoryId.message?.toString()}
                                        </p>
                                    )}
                                </div>
                            </div>
                            <div className="mt-3">
                                <button className="fs-15px fw-bold btn btn-dark bg-blue-800">
                                    Edit SubCategory
                                </button>
                                <button className="fw-bold btn btn-secondary mx-2"
                                    onClick={() => navigate("/subCategory")}>
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

export default EditSubCategory;
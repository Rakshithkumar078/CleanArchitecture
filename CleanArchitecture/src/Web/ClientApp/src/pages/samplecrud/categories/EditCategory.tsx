import { Link, useNavigate, useParams } from "react-router-dom";
import { useForm } from "react-hook-form";
import CategoryService from "../shared/services/CategoryService";
import SweetAlertService from "../../../services/SweetAlertService";
import { Spinner } from "react-bootstrap";
import { useEffect, useState } from "react";
import { Category_Regex, errorMessages, getMaxLengthErrorMessage, getRequiredErrorMessage, max_limit_category } from "../shared/constants/AppConstant";

const EditCategory = () => {
    const [isShowLoader, setIsShowLoader] = useState(false);
    const navigate = useNavigate();
    const { id } = useParams();
    const { register, handleSubmit, setValue, reset, formState: { errors }, } = useForm();

    //method to get category details by id
    const getCategoryById = async () => {
        try {
            setIsShowLoader(true);
            const response = await CategoryService.getCategoryById(Number(atob(id ?? '')));
            setValue('category', response?.data?.name);
            setIsShowLoader(false);
        } catch (error) {
            setIsShowLoader(false);
            SweetAlertService.error(
                "Error",
                `Something went wrong while getting category details`
            );
        }
    };

    useEffect(() => {
        getCategoryById();
    }, []);

    //method to submit the category form
    const onSubmit = async (data: any) => {
        const postData = {
            name: data.category.trim(),
            id: Number(atob(id ?? ''))
        };
        try {
            setIsShowLoader(true);
            const response = await CategoryService.updateCategory(postData);
            if (response.data) {
                setIsShowLoader(false);
                reset();
                await SweetAlertService.success(
                    "Success",
                    "Category updated successfully"
                );
                navigate("/category");
            }
        } catch (error: any) {
            setIsShowLoader(false);
            console.log("g",error)
            if (error?.response?.data?.errors.Name[0]) {
                SweetAlertService.error("Error", error.response.data.errors.Name[0]);
            } else {
                SweetAlertService.error("Error", "Something went wrong while updating category");
            }
        }
    };

    return (
        <>
            {isShowLoader ? (<div className="loader-backdrop">
                <Spinner animation="border" variant="primary" />
            </div>) : (<>
                <div className="panel-body">
                    <div className="header-panel d-flex justify-content-between">
                            <h1>Edit Category {"hi"}</h1>
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
                                            <b>Edit Category</b>
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
                                        Edit Category
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
            </>)}
        </>
    );
};

export default EditCategory;
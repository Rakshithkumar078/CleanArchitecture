import { useEffect, useState } from "react";
import { useNavigate } from "react-router";
import SweetAlertService from "../../../services/SweetAlertService";
import { DeletePopupConfirmationMessage } from "../shared/constants/AppConstant";
import { SubCategory } from "./models/SubCategory";
import SubCategoryService from "../shared/services/SubCategoryService";

const SubCategoryList = () => {
    const navigate = useNavigate();
    const [subCategories, setSubCategories] = useState<SubCategory[]>([]);

    //method to get categories through API
    const getAllSubCategories = async () => {
        try {
            const response = await SubCategoryService.getAllSubCategories();
            setSubCategories(response.data);
        } catch (error) {
            SweetAlertService.error(
                "Error",
                "Something went wrong while fetching sub-categories"
            );
        }
    };

    //method to get ID of seleced row and modal visibilty for delete purpose
    const onDeleteClick = (id: number) => {
        SweetAlertService.confirm('Delete', DeletePopupConfirmationMessage).then(async (res) => {
            if (res.isConfirmed) {
                try {
                    const response = await SubCategoryService.deleteSubCategory(id);
                    if (response.data) {
                        await SweetAlertService.success("Success", "SubCategory deleted successfully");
                        getAllSubCategories();
                    }
                } catch (error: any) {
                    SweetAlertService.error("Error", "Something went wrong while deleting the sub-category");
                }
            }
        })
    };

    useEffect(() => {
        getAllSubCategories();
    }, []);

    return (
        <>
            <div className="row p-1 col-md-12">
                <div className="col-md-8">
                    <h1>Sub Categories</h1>
                </div>
                <div className="col-md-4 d-flex justify-content-end">
                    <button
                        className="fw-bold btn btn-dark bg-blue-800 me-3"
                        onClick={() => navigate("/createSubCategory")}
                    >
                        Create SubCategory
                    </button>
                </div>
            </div>
            <div className="panel table-responsive content mt-2">
                {
                    subCategories.length > 0 ? (<>
                        <table className="table table-bordered table table-striped">
                            <thead>
                                <tr>
                                    <th scope="col">ID</th>
                                    <th scope="col">Name</th>
                                    <th scope="col">Category</th>
                                    <th scope="col"></th>
                                </tr>
                            </thead>
                            <tbody>
                                {subCategories.map((subCategory, index) => (
                                    <tr key={subCategory.id}>
                                        <th scope="row">{index + 1}</th>
                                        <td>{subCategory?.name}</td>
                                        <td>{subCategory?.categories?.name}</td>
                                        <td>
                                            <button
                                                className="fw-bold btn btn-dark bg-blue-800 me-3"
                                                onClick={() => navigate(`/updatesubcategory/${btoa(subCategory?.id.toString())}`)}
                                            >
                                                Edit
                                            </button>
                                            <button
                                                className="fw-bold btn btn-secondary"
                                                onClick={() => onDeleteClick(subCategory.id)}
                                            >
                                                Delete
                                            </button>
                                        </td>
                                    </tr>
                                ))}
                            </tbody>
                        </table>
                    </>) : (<>
                        <h4>No sub-categories found.</h4>
                    </>)
                }
            </div>
        </>
    );
}

export default SubCategoryList;

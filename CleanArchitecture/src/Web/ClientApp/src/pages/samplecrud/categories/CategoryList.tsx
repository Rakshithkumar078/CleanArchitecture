import { useEffect, useState } from "react";
import { Category } from "./models/Category";
import { DeletePopupConfirmationMessage } from "../shared/constants/AppConstant";
import CategoryService from "../shared/services/CategoryService";
import SweetAlertService from "../../../services/SweetAlertService";
import { useNavigate } from "react-router";
import { Spinner } from "react-bootstrap";

const CategoryList = () => {
    const navigate = useNavigate();
    const [categories, setCategories] = useState<Category[]>([]);
    const [isShowLoader, setIsShowLoader] = useState(false);

    //method to get all categories
    const getAllCategories = async () => {
        try {
            setIsShowLoader(true);
            const response = await CategoryService.getAllCategories();
            setCategories(response.data);
            setIsShowLoader(false);
        } catch (error) {
            setIsShowLoader(false);
            SweetAlertService.error(
                "Error",
                "Something went wrong while fetching categories"
            );
        }
    };

    //method to delete category
    const onDeleteClick = (id: number) => {
        SweetAlertService.confirm('Delete', DeletePopupConfirmationMessage).then(async (res) => {
            if (res.isConfirmed) {
                try {
                    const response = await CategoryService.deleteCategory(id);
                    if (response.data) {
                        await SweetAlertService.success("Success", "Category deleted successfully");
                        getAllCategories();
                    }
                } catch (error: any) {
                    SweetAlertService.error("Error", "Something went wrong while deleting the category");
                }
            }
        })
    };

    useEffect(() => {
        getAllCategories();
    }, []);

    return (
        <>
            {isShowLoader ? (<div className="loader-backdrop">
                <Spinner animation="border" variant="primary" />
            </div>) : (<>
                <div className="row p-1 col-md-12">
                    <div className="col-md-8">
                        <h1>Categories</h1>
                    </div>
                    <div className="col-md-4 d-flex justify-content-end">
                        <button
                                className="fw-bold btn btn-dark bg-blue-800 me-3"
                            onClick={() => navigate("/createCategory")}
                        >
                            Create Category
                        </button>
                    </div>
                </div>
                <div className="panel table-responsive content mt-2">
                    {
                        categories.length > 0 ? (<>
                            <table className="table table-bordered table table-striped">
                                <thead>
                                    <tr>
                                        <th scope="col">ID</th>
                                        <th scope="col">Category</th>
                                        <th scope="col">Sub Category</th>
                                        <th scope="col"></th>
                                    </tr>
                                </thead>
                                <tbody>
                                    {categories.map((category, index) => (
                                        <tr key={category?.id}>
                                            <th scope="row">{index + 1}</th>
                                            <td>{category?.name}</td>
                                            <td>
                                                {category?.subCategories.map((subcategory) => (
                                                    <p key={subcategory.id}>{subcategory?.name}</p>
                                                ))}
                                            </td>
                                            <td>
                                                <button
                                                    className="fw-bold btn btn-dark bg-blue-800 me-3"
                                                    onClick={() => navigate(`/updatecategory/${btoa(category?.id.toString())}`)}
                                                >
                                                    Edit
                                                </button>
                                                <button
                                                    className="fw-bold btn btn-secondary"
                                                    onClick={() => onDeleteClick(category.id)}
                                                >
                                                    Delete
                                                </button>
                                            </td>
                                        </tr>
                                    ))}
                                </tbody>
                            </table>
                        </>) : (<>
                            <h4>No categories found.</h4>
                        </>)
                    }
                </div>
            </>)}
        </>
    );
}

export default CategoryList;
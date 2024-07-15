import api from "../../../../store/AuthHeader";

const getAllSubCategories = () => {
    return api.get("/SubCategory/GetAllSubCategories");
};

const postSubCategory = (data: any) => {
    return api.post("/SubCategory/CreateSubCategory", data);
};

const updateSubCategory = (data: any) => {
    return api.put("/SubCategory/UpdateSubCategory", data);
};

const deleteSubCategory = (id: number) => {
    return api.delete(`/SubCategory/DeleteSubCategory/${id}`);
};

const getSubCategoryById = (id: number) => {
    return api.get(`/SubCategory/GetSubCategoryById/${id}`);
};

export default {
    getAllSubCategories,
    postSubCategory,
    updateSubCategory,
    deleteSubCategory,
    getSubCategoryById,
};
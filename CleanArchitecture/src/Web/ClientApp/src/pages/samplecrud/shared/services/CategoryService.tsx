import api from "../../../../store/AuthHeader";

const getAllCategories = () => {
  return api.get("/Category/GetAllCategories");
};

const postCategories = (data: any) => {
  return api.post("/Category/CreateCategory", data);
};

const updateCategory = (data: any) => {
  return api.put("/Category/UpdateCategory", data);
};

const deleteCategory = (id: number) => {
  return api.delete(`/Category/DeleteCategory/${id}`);
};

const getCategoryById = (id: number) => {
    return api.get(`/Category/GetCategoryById/${id}`);
};

export default {
  getAllCategories,
  postCategories,
  updateCategory,
  deleteCategory,
  getCategoryById,
};
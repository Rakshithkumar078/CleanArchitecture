import Home from "./components/Home";
import CategoryList from "./pages/samplecrud/categories/CategoryList";
import CreateCategory from "./pages/samplecrud/categories/CreateCategory";
import EditCategory from "./pages/samplecrud/categories/EditCategory";
import CreateSubCategory from "./pages/samplecrud/subcatgories/CreateSubCategory";
import EditSubCategory from "./pages/samplecrud/subcatgories/EditSubCategory";
import SubCategoryList from "./pages/samplecrud/subcatgories/SubCategoryList";

const AppRoutes = [
    {
        index: true,
        element: <Home />
    },
    {
        path: '/category',
        element: <CategoryList />
    },
    {
        path: '/createCategory',
        element: <CreateCategory />
    },
    {
        path: '/updatecategory/:id',
        element: <EditCategory />
    },
    {
        path: '/subCategory',
        element: <SubCategoryList />
    },
    {
        path: '/createSubCategory',
        element: <CreateSubCategory />
    },
    {
        path: '/updatesubcategory/:id',
        element: <EditSubCategory />
    },
];

export default AppRoutes;
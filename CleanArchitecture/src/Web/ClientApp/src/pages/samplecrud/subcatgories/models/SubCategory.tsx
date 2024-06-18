//interface to define the shape of all categories
export interface SubCategory {
    id: number;
    name: string;
    categories: {
        id: number;
        name: string;
    };
}
//interface to define the shape of all categories
export interface Category {
    id: number;
    name: string;
    subCategories: [
        {
            id: number;
            name: string;
            categoryId: number;
        }
    ];
}
//Conformation messgae for deleteing any element 
export const DeletePopupConfirmationMessage = "Are you sure you want to delete?";

//required validation message
export const getRequiredErrorMessage = (fieldName: any) => {
    return `The ${fieldName} field is required`;
};

//validation to allow only alphabets in the input filed
export const Category_Regex = /^[a-zA-Z]+(?:\s[a-zA-Z]+)*$/;

//vaidation error if input field has numberic characters
export const errorMessages = {
    pattern: "These characters are not allowed",
};

//max length validation limits
export const max_limit_category = 25;
export const max_limit_Subcategory = 35;

//max length validation message
export const getMaxLengthErrorMessage = (fieldName: any, maxLength: any) => {
    return `${fieldName} should not exceed ${maxLength} characters`;
};
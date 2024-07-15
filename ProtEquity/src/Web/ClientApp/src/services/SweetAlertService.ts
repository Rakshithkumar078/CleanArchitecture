import Swal from "sweetalert2";

const swalWithBootstrapButtons = Swal.mixin({
    customClass: {
        confirmButton: "btn btn-primary ms-2",
        cancelButton: "btn btn-secondary",
    },
    buttonsStyling: false,
});

const SweetAlertService = {
    success: (title: any, message: any) => {
        return swalWithBootstrapButtons.fire({
            title: title,
            text: message,
            icon: 'success',
            showConfirmButton: true,
            //  timer: 1500,
        });
    },

    error: (title: any, message: any) => {
        return swalWithBootstrapButtons.fire({
            title: title,
            text: message,
            icon: 'error',
            confirmButtonText: 'OK'
        });
    },
    errorWithHTML: (title: any, message: any) => {
        return swalWithBootstrapButtons.fire({
            title: title,
            html: message,
            icon: 'error',
            confirmButtonText: 'OK'
        });
    },
    confirm: (title: any, message: any) => {
        return swalWithBootstrapButtons.fire({
            title: title,
            text: message,
            icon: 'warning',
            showCancelButton: true,
            confirmButtonText: 'Confirm',
            cancelButtonText: 'Cancel',
            reverseButtons: true
        });
    },
    confirmCustom: (
        title: any,
        message: any,
        confirmButtonText: any,
        cancelButtonText: any
    ) => {
        return swalWithBootstrapButtons.fire({
            title: title,
            text: message,
            icon: "warning",
            showCancelButton: true,
            confirmButtonText: confirmButtonText,
            cancelButtonText: cancelButtonText,
            reverseButtons: true,
        });
    },
    successCustom: (
        title: string,
        message: string,
        showConfirmButton = false,
        ...rest: any[]
    ) => {
        return swalWithBootstrapButtons.fire({
            title: title,
            text: message,
            icon: "success",
            showConfirmButton,
            ...rest,
        });
    },
    successWithNavigation: (title: any, message: any, nextPageURL: any) => {
        return swalWithBootstrapButtons.fire({
            title: title,
            text: message,
            icon: 'success',
            showConfirmButton: true,
        }).then((result) => {
            if (result.isConfirmed) {
                window.location.href = nextPageURL; // Redirect to the next page
            }
        });
    },
};

export default SweetAlertService;
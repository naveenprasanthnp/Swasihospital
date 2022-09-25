function ShowErrorMessage(message) {
    alert("Ye");
    debugger;
    Lobibox.notify(
        'error',
        {
            title: "Error",
            pauseDelayOnHover: true,
            continueDelayOnInactiveTab: false,
            msg: message,
            icon: false,
            sound: false
        }
    );
}

function ShowSuccessMessage(message) {
    Lobibox.notify(
        'success',
        {
            title: "Success",
            pauseDelayOnHover: true,
            continueDelayOnInactiveTab: false,
            msg: message,
            icon: false,
            sound: false
        }
    );
}
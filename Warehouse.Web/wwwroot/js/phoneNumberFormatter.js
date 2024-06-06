window.formatPhoneNumber = (element) => {
    element.addEventListener('input', (event) => {
        let input = event.target.value.replace(/\D/g, ''); // Remove all non-digit characters
        if (input.length > 3 && input.length <= 6) {
            input = input.replace(/(\d{3})(\d{0,3})/, '$1-$2');
        } else if (input.length > 6) {
            input = input.replace(/(\d{3})(\d{3})(\d{0,4})/, '$1-$2-$3');
        }
        event.target.value = input;
    });
};

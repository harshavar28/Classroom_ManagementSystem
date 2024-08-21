document.addEventListener("DOMContentLoaded", function () {
    // Example: Confirm before submission
    const form = document.querySelector("form");
    form.addEventListener("submit", function (event) {
        if (!confirm("Are you sure you want to save the attendance?")) {
            event.preventDefault(); // Prevent form submission if the user cancels
        }
    });

    // Example: You could add more custom JS here as needed
});

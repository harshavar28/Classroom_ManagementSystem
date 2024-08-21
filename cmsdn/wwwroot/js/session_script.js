document.addEventListener("DOMContentLoaded", function () {
    // Handle delete session button click
    document.querySelectorAll(".delete-session-button").forEach(button => {
        button.addEventListener("click", function () {
            const row = this.closest('tr');
            const sessionId = row.getAttribute('data-session-id');

            console.log('Session ID:', sessionId); // Debugging line

            if (confirm('Are you sure you want to delete this session?')) {
                fetch(`/Session/DeleteSession/${sessionId}`, {
                    method: 'POST',
                    headers: {
                        'RequestVerificationToken': document.querySelector('input[name="__RequestVerificationToken"]').value
                    }
                }).then(response => {
                    console.log('Fetch response:', response); // Debugging line

                    if (response.ok) {
                        row.remove(); // Remove the row from the table
                    } else {
                        alert('Error deleting session.');
                    }
                }).catch(error => {
                    console.error('Error:', error);
                    alert('Error deleting session.');
                });
            }
        });
    });

    // Handle search and filter functionality
    document.getElementById("searchButton").addEventListener("click", function () {
        const searchName = document.getElementById("searchName").value.toLowerCase();
        const filterClass = document.getElementById("filterClass").value;

        let rows = document.querySelectorAll("#sessionTableBody tr");
        rows.forEach(row => {
            const teacherName = row.cells[2].innerText.toLowerCase();
            const sessionClass = row.cells[3].innerText;

            if ((teacherName.includes(searchName) || searchName === "") &&
                (sessionClass === filterClass || filterClass === "")) {
                row.style.display = "";
            } else {
                row.style.display = "none";
            }
        });
    });

    document.getElementById("filterClass").addEventListener("change", function () {
        document.getElementById("searchButton").click();
    });
});

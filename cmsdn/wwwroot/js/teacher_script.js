document.getElementById('searchButton').addEventListener('click', function () {
    const searchName = document.getElementById('searchName').value.toLowerCase();
    const filterDepartment = document.getElementById('filterDepartment').value;

    let rows = document.querySelectorAll('#teacherTableBody tr');
    rows.forEach(row => {
        const name = row.cells[0].innerText.toLowerCase();
        const department = row.cells[2].innerText;

        if ((name.includes(searchName) || searchName === "") &&
            (department === filterDepartment || filterDepartment === "")) {
            row.style.display = "";
        } else {
            row.style.display = "none";
        }
    });
});

document.getElementById('filterDepartment').addEventListener('change', function () {
    document.getElementById('searchButton').click();
});

document.querySelectorAll('.delete-teacher-button').forEach(button => {
    button.addEventListener('click', function () {
        const row = this.closest('tr');
        const teacherId = row.getAttribute('data-teacher-id');

        console.log('Teacher ID:', teacherId); // Debugging line

        if (confirm('Are you sure you want to delete this teacher?')) {
            fetch(`/Teacher/DeleteTeacher/${teacherId}`, {
                method: 'POST',
                headers: {
                    'RequestVerificationToken': document.querySelector('input[name="__RequestVerificationToken"]').value
                }
            }).then(response => {
                console.log('Fetch response:', response); // Debugging line

                if (response.ok) {
                    row.remove(); // Remove the row from the table
                } else {
                    alert('Error deleting teacher.');
                }
            }).catch(error => {
                console.error('Error:', error);
                alert('Error deleting teacher.');
            });
        }
    });
});



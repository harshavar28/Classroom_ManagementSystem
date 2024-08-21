document.addEventListener("DOMContentLoaded", function () {
    // Handle search and filter functionality
    document.getElementById("searchButton").addEventListener("click", function () {
        const searchName = document.getElementById("searchName").value.toLowerCase();
        const filterClass = document.getElementById("filterClass").value;

        filterSessions(searchName, filterClass);
    });

    document.getElementById("searchName").addEventListener("input", function () {
        const searchName = document.getElementById("searchName").value.toLowerCase();
        const filterClass = document.getElementById("filterClass").value;

        filterSessions(searchName, filterClass);
    });

    document.getElementById("filterClass").addEventListener("change", function () {
        const searchName = document.getElementById("searchName").value.toLowerCase();
        const filterClass = document.getElementById("filterClass").value;

        filterSessions(searchName, filterClass);
    });

    function filterSessions(searchName, filterClass) {
        let rows = document.querySelectorAll("#sessionTableBody tr");
        rows.forEach(row => {
            const subject = row.cells[3].innerText.toLowerCase();
            const sessionClass = row.cells[2].innerText;

            if ((subject.includes(searchName) || searchName === "") &&
                (sessionClass === filterClass || filterClass === "")) {
                row.style.display = "";
            } else {
                row.style.display = "none";
            }
        });
    }
});

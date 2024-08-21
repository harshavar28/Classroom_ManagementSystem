// index_script.js
document.getElementById('Role').addEventListener('change', function () {
    var adminCodeGroup = document.getElementById('adminCodeGroup');
    if (this.value === 'Admin') {
        adminCodeGroup.style.display = 'block';
    } else {
        adminCodeGroup.style.display = 'none';
    }
});

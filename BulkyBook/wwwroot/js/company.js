var dataTable;
$(document).ready( function () {
  loadDataTable();
} );

function loadDataTable() {
  dataTable = $('#CompanyTable').DataTable({
    "ajax": {
      "url":"/Admin/Companies/GetAll"
    },
    "columns": [
      { "data": "name", "width": "15%" },
      { "data": "streetAddress", "width": "20%" },
      { "data": "city", "width": "15%" },
      { "data": "state", "width": "5%" },
      { "data": "postalCode", "width": "10%" },
      { "data": "phoneNumber", "width": "15%" },
      { 
        "data": "id", 
        "render": function(id) {
          return `
           <div class="text-right">
            <a href="/Admin/Companies/Upsert/${id}" class="text-warning">
              <i class="bi bi-pencil me-1"></i> Edit
            </a>
            <span class="me-1 ms-2">|</span>
            <form action="/Admin/Companies/Delete?id=${id}" method="post" style="display:inline-block">
              <button type="submit" onclick="return confirm('Are you sure?');" class="link p-0">
                <i class="bi bi-trash me-1"></i> Delete
              </button>
            </form>
          </div>
          `
        }, 
        "width": "20%" 
      },
    ]
  });
  
}
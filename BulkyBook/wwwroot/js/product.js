var dataTable;
$(document).ready( function () {
  loadDataTable();
} );

function loadDataTable() {
  dataTable = $('#ProductTable').DataTable({
    "ajax": {
      "url":"/Admin/Products/GetAll"
    },
    "columns": [
      { "data": "name", "width": "15%" },
      { "data": "listPrice", "width": "15%" },
      { "data": "price", "width": "10%" },
      { "data": "category.name", "width": "15%" },
      { "data": "coverType.name", "width": "15%" },
      { 
        "data": "imageUrl", 
        "render": function(url) {
          return `
           <img alt="image" width="auto" height="30" class="" src="${url}"/>
          `
        }, 
        "width": "5%" 
      },
      { 
        "data": "id", 
        "render": function(id) {
          return `
           <div class="text-right">
           <a href="/Admin/Products/${id}" class="text-primary">
              <i class="bi bi-pencil me-1"></i> View
            </a>
            <span class="me-1 ms-2">|</span>
            <a href="/Admin/Products/Upsert/${id}" class="text-warning">
              <i class="bi bi-pencil me-1"></i> Edit
            </a>
            <span class="me-1 ms-2">|</span>
            <form action="/Admin/Products/Delete?id=${id}" method="post" style="display:inline-block">
              <button type="submit" onclick="return confirm('Are you sure?');" class="link p-0">
                <i class="bi bi-trash me-1"></i> Delete
              </button>
            </form>
          </div>
          `
        }, 
        "width": "25%" 
      },
    ]
  });
  
}
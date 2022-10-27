var dataTable;
$(document).ready( function () {
  var url = window.location.search;
  if (url.includes('all')) { url = '' }
  loadDataTable(url);
} );

function loadDataTable(status) {
  console.log("/Admin/Orders/GetAll" + status)
  dataTable = $('#OrdersTable').DataTable({
    "ajax": {
      "url":"/Admin/Orders/GetAll" + status
    },
    "columns": [
      { "data": "id", "width": "5%" },
      { "data": "name", "width": "15%" },
      { "data": "phoneNumber", "width": "20%" },
      { "data": "applicationUser.email", "width": "15%" },
      { "data": "orderStatus", "width": "15%" },
      { "data": "orderTotal", "width": "10%" },
      { 
        "data": "id", 
        "render": function(id) {
          return `
           <div class="text-right">
            <a href="/Admin/Orders/Details?id=${id}" class="text-dark">
              <i class="bi bi-file-earmark-text me-1"></i> View
            </a>
          </div>
          `
        }, 
        "width": "20%" 
      },
    ]
  });
  
}
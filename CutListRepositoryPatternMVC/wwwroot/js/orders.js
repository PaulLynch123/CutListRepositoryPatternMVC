//job.js file to run javascript for dataTables (API)

var dataTable;

$(document).ready(function () {
    //deciding which controller API action to call

    //url of object
    var url = window.location.search;
    //if string 'approved' appears in url
    if (url.includes("approved")) {
        loadDataTable("GetAllApprovedOrders");
    }
    else {
        if (url.includes("pending")) {
            loadDataTable("GetAllPendingOrders");
        }
        //if 'approved' or 'pending' do not appear then
        else {
            loadDataTable("GetAllOrders");
        }
    }
});

//site to get dataTables info --- http://cdn.datatables.net/ OR https://datatables.net/ ----

//pass the para from above
function loadDataTable(url) {
    //id from cshtml
    dataTable = $('#tblData').DataTable({
        "ajax": {
            //path to method FORMAT area/controller/method (method comming from the url variable)
            "url": "/admin/order/" + url,
            "type": "GET",
            "datatype": "json"
        },
        //info from your model
        //in an array
        "columns": [
            { "data": "name", "width": "20%" },
            { "data": "phone", "width": "20%" },
            { "data": "email", "width": "20%" },
            { "data": "serviceCount", "width": "10%" },
            { "data": "status", "width": "10" },
            //editing and deleteing info
            {
                "data": "id",
                "render": function (data) {
                    //tidl notation for string... inside which I have css html using font awesome icons
                    //edit is href but Delete is onClick calling JS function Delete passing param of path to API
                    return `
                            <div class="text-center">
                                <a href="/Admin/order/Details/${data}" class='btn btn-success text-white' style='cursor:pointer; width:100px;'>
                                    <i class='fas fa-edit'></i> Details
                                </a>
                               
                            </div>
                            `;
                }, "width": "20%"
            }
        ],
        //no records exist
        "language": {
            "emptyTable": "No Records Found"
        },
        "width": "100%"
    });
}

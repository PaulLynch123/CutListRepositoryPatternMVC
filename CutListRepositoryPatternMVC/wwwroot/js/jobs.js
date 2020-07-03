//job.js file to run javascript for dataTables (API)

var dataTable;

$(document).ready(function () {
    loadDataTable();
})

//site to get dataTables info --- http://cdn.datatables.net/ OR https://datatables.net/ ----
function loadDataTable() {
    //id from cshtml
    dataTable = $('#tblData').DataTable({
        "ajax": {
            //path to method FORMAT area/controller/method
            "url": "/admin/job/GetAll",
            "tye": "GET",
            "datatype": "json"
        },
        //info from your model
        //in an array
        "columns": [
            { "data": "won", "width": "50%" },
            { "data": "project_Name", "width": "20%" },
            //editing and deleteing info
            {
                "data": "id",
                "render": function (data) {
                    //tidl notation for string... inside which I have css html using font awesome icons
                    //edit is href but Delete is onClick calling action Delete passing param of path
                    return `
                            <div class="text-center">
                                <a href="/admin/job/Upsert/$data" class='btn btn-success text-white' style='cursor:pointer; width:100px;'>
                                    <i class='far fa edit'></i> Edit
                                </a>
                                &nbsp;
                                <a onclick=Delete("/admin/job/Delete/$data") class='btn btn-danger text-white' style='cursor:pointer; width:100px;'>
                                    <i class='far fa trash-alt'></i> Delete
                                </a>
                            </div>
                            `;
                }, "width": "30%"
            }
        ],
        //no records exist
        "language": {
            "emptyTable": "No Records Found"
        },
        "width": "100%"
    })
}

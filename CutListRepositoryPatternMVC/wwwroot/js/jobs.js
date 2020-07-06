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
            "type": "GET",
            "datatype": "json"
        },
        //info from your model
        //in an array
        "columns": [
            { "data": "id", "width": "10%" },
            { "data": "won", "width": "20%" },
            { "data": "project_Name", "width": "40%" },
            //editing and deleteing info
            {
                "data": "id",
                "render": function (data) {
                    //tidl notation for string... inside which I have css html using font awesome icons
                    //edit is href but Delete is onClick calling JS function Delete passing param of path to API
                    return `
                            <div class="text-center">
                                <a href="/Admin/job/Upsert/${data}" class='btn btn-success text-white' style='cursor:pointer; width:100px;'>
                                    <i class='fas fa-edit'></i> Edit
                                </a>
                                &nbsp;
                                <a onclick=Delete("/Admin/job/Delete/${data}") class='btn btn-danger text-white' style='cursor:pointer; width:100px;'>
                                    <i class='fas fa-trash-alt'></i> Delete
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
    });
}

function Delete(url) {
    // pop-up using sweetAlert.js.org
    //popup with call to Delete action in API
    swal({
        title: "Are you sure you want to delete?",
        text: "you will be unable to restore content after",
        type: "warning",
        showCancelButton: true,
        confirmButtonColour: "#e3492b",
        confirmButtonText: "yep, delete it",
        closeOnconfirm: true
    }, function () {
            $.ajax({
                //call delete
                type: 'DELETE',
                url: url,
                //if request succeeds run function
                success: function (data) {
                    if (data.success) {
                        //call 'ShowMessage' function
                        ShowMessage(data.message);
                        dataTable.ajax.reload();
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            })
    }

    )
}

function ShowMessage(msg) {
    //using toastr framework
    //get the message from Model validation
    toastr.success(msg);
}
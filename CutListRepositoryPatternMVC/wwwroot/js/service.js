//service.js file to run javascript for dataTables (API)

var dataTable;

//load dataTable after DOM is fully loaded
$(document).ready(function () {
    loadDataTable();
});


//site to get dataTables info --- http://cdn.datatables.net/ OR https://datatables.net/ ----
function loadDataTable() {
    //id from cshtml attach to the table in the Index with id=#tblData
    dataTable = $('#tblData').DataTable({
        "ajax": {
            //attributes to set
            //path to method FORMAT area/controller/method
            "url": "/admin/service/GetAll",
            "type": "GET",
            "datatype": "json"
        },
        //info from your model
        //in an array
        "columns": [
            { "data": "name", "width": "20%" },
            { "data": "job.project_Name", "width": "20%" },
            { "data": "price", "width": "15%" },
            { "data": "frequency.frequencyCount", "width": "10%" },
            //editing and deleteing info
            {
                "data": "id",
                "render": function (data) {
                    //tidl notation for string... inside which I have css html using font awesome icons
                    //edit is href but Delete is onClick calling JS function Delete passing param of path to API
                    return `
                            <div class="text-center">
                                <a href="/Admin/service/Upsert/${data}" class='btn btn-success text-white' style='cursor:pointer; width:100px;'>
                                    <i class='fas fa-edit'></i> Edit
                                </a>
                                &nbsp;
                                <a class='btn btn-danger text-white' style='cursor:pointer; width:100px;' onclick=Delete("/Admin/service/Delete/${data}") >
                                    <i class='fas fa-trash-alt'></i> Delete
                                </a>
                            </div>
                            `;
                }, "width": "30%"
            }

        ],
        //no records exist 
        //NOTE TO SELF-------NOT SURE WHAT LANGUAGE DOES...CHECK IT OUT
        "language": {
            "emptyTable": "No Records Found. 'Service'."
        },
        "width": "100%"
    });
}


function Delete(url) {
    // pop-up using sweetAlert.js.org
    //popup with call to Delete action in API
    swal({
        title: "Are you sure you want to delete?",
        text: "you will be unable to restore file after!",
        type: "warning",
        showCancelButton: true,
        confirmButtonColour: "#DD6B55",
        confirmButtonText: "Go ahead, delete it",
        closeOnconfirm: true
    }, function () {
        $.ajax({
            //http delete type
            type: 'DELETE',
            url: url,
            //if request succeeds run function
            success: function(data) {
                if (data.success) {
                    //call 'ShowMessage' function
                    ShowMessage(data.message);
                    dataTable.ajax.reload();
                }
                else {
                    toastr.error(data.message);
                }
            }
        });

    });

}

function ShowMessage(msg) {
    //using toastr framework
    //get the message from Model validation
    toastr.success(msg);
}
var dataTable;

$(document).ready(function () {
    LoadTable();
});

function LoadTable() {
    if (dataTable)
        dataTable.destroy();

    dataTable = $("#DataTable").DataTable({
        "ajax": {
            "url": "/Properties/GetAll",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "id", "width": "5%" },
            { "data": "title", "width": "15%" },
            { "data": "address", "width": "15%" },
            { "data": "description", "width": "15%" },
            { "data": "status.description", "width": "15%" },

            {
                "data": "id",
                "render": function (data) {
                    return `<div class="text-center">
                            <a href='/Properties/Edit/${data}' class='btn btn-success text-white' style='cursor:pointer; width:100px;'>
                             Editar
                            </a>
                            &nbsp;
                            <a onclick=Delete(${data})  class='btn btn-danger text-white' style='cursor:pointer; width:100px;'>
                             Borrar
                            </a>
                            `;
                }, "width": "30%"
            }
        ],

        "width": "100%"
    });
}
//href='/Properties/Delete/${data}'
function Delete(id) {
        $.post({
            type: 'POST',
            url: "/Properties/Delete/"+id,
            success: function (data) {
                if (data) {
                    alert("No se peude borrar pripiedades si existe una actividad con ella");
                }
                else {
                    DeleteConfirm(id);
                }
            }
        });
}

function DeleteConfirm(id) {
    $.post({
        type: 'DELETE',
        url: "/Properties/DeleteConfirm/" + id,
        success: function (data) {
            LoadTable();
        }

    });
}

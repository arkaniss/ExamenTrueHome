var dataTable;

$(document).ready(function () {
    LoadTable();
});

function LoadTable() {
    dataTable = $("#DataTable").DataTable({
        "ajax": {
            "url": "/Home/GetAllCondition",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "id", "width": "5%" },
            { "data": "formtDateSchedule", "width": "15%" },
            { "data": "title", "width": "15%" },
            { "data": "formatDateCreateAt", "width": "15%" },
            { "data": "status.description", "width": "15%" },
            { "data": "condition", "width": "15%" },

            {
                "data": "id",
                "render": function (data) {
                    return `<div class="text-center">
                            <a href='/Home/EditReAgendar/${data}' class='btn btn-success text-white' style='cursor:pointer; width:100px;'>
                             Re-Agendar
                            </a>
                            &nbsp;
                            <a href='/Home/Cancel/${data}' class='btn btn-danger text-white' style='cursor:pointer; width:100px;'>
                             Cancelar
                            </a>
                            &nbsp;
                            <a href='/Home/Survey/${data}' class='btn btn-primary text-white' style='cursor:pointer; width:100px;'>
                             Encuesta
                            </a>
                            `;
                }, "width": "30%"
            }
        ],

        "width": "100%"
    });
}
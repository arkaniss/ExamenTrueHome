var dataTable;

$(document).ready(function () {
    LoadTable();
    GetDropDown();
    $("#BtnAdvancedFilter").click(function () {
        LoadTableFilter();
    });
});

function LoadTable() {
    dataTable = $("#DataTable").DataTable({
        "ajax": {
            "url": "/Home/GetAllFilter",
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
                            <a href='/Home/Edit/${data}' class='btn btn-success text-white' style='cursor:pointer; width:100px;'>
                             Re-Agendar
                            </a>
                            &nbsp;
                            <a onclick=Delete("/Properties/Delete/${data}") class='btn btn-danger text-white' style='cursor:pointer; width:100px;'>
                             Cancelar
                            </a>
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

function LoadTableFilter() {
    if ($("#start").val() == '' || $("#end").val() == '') {
        alert("Selecciona el rango de fechas");
        return;
    }
    
    var start = new Date($("#start").val()).toISOString();
    var end = new Date($("#end").val()).toISOString();
    var status = $("#Status").val();

    if (start > end) {
        alert("La fecha de inicio no puede ser amyor a la fecha fin");
        return;
    }

    $.post('/Home/GetAllFilterData', { start: start, end: end,status :status },
        function (result) {
            if (dataTable)
                dataTable.destroy();

            dataTable =  $('#DataTable').DataTable({
                data: result,
                columns: [
                    { "data": "id", "visible": false, "searchable": false },
                    { "data": 'formtDateSchedule' },
                    { "data": 'title' },
                    { "data": 'formatDateCreateAt' },
                    { "data": 'status.description' },
                    { "data": 'condition' },
                    {
                        "data": "id",
                        "render": function (data) {
                            return `<div class="text-center">
                            <a href='/Home/Edit/${data}' class='btn btn-success text-white' style='cursor:pointer; width:100px;'>
                             Re-Agendar
                            </a>
                            &nbsp;
                            <a onclick=Delete("/Properties/Delete/${data}") class='btn btn-danger text-white' style='cursor:pointer; width:100px;'>
                             Cancelar
                            </a>
</a>
                            &nbsp;
                            <a href='/Home/Survey/${data}' class='btn btn-primary text-white' style='cursor:pointer; width:100px;'>
                             Encuesta
                            </a>
                            `;
                        }, "width": "30%"
                    }
                ]
            });
    
    });
 
}

function GetDropDown() {
    $.ajax({
        type: "GET",
        url: "/Home/GetDataStatus",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data)
    {
        $.each(data, function () {
            $("#Status").append($("<option/>").val(this.value).text(this.text));
        });
    },
    failure: function () {
        alert("Failed!");
    }
});
}
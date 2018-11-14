$(document).ready(function () {
    CargarFecha('txtFechaIncial', 'yy-mm-dd');
    CargarFecha('txtFechaFinal', 'yy-mm-dd');
    ConsultarEventos();
});

function ConsultarEventos() {
    debugger;
    var dataColumns = [
        { data: "IdLogApp", ctroFilter: "txtNombreFilter" },
        { data: "CreationDate", ctroFilter: "txtNombreFilter" },
        { data: "WebServiceName", ctroFilter: "txtNombreFilter" },
        { data: "MethodNameUI", ctroFilter: "txtNombreFilter" },
        { data: "Data", ctroFilter: "txtNombreFilter" },
    ];
    SetDataTable("tblLogEventos", ServiceUrl + "Home/AjaxGetJsonDataLTLogApp", dataColumns);
}

function Buscar() {
    $('#dvLogEventos').show();
    $('#tblLogEventos').DataTable().search(
       JSON.stringify({
           search: {
               FechaInicial: $("#txtFechaIncial").val(),
               FechaFinal: $("#txtFechaFinal").val()
           }
       })
    ).draw();
}

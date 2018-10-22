$(document).ready(function () {
    ConsultarEventos();
    //BuscarTable("Buscartxt", "tbodyLogEventos");
});
var ListEventos = new Array();
function ConsultarEventos() {
    debugger;
    var dataColumns = [
        { data: "RUsuario.RCCF.Nombre", ctroFilter: "txtNombreFilter" },
        { data: "RUsuario.Nombres", ctroFilter: "txtNombreFilter" },
        { data: "Evento", ctroFilter: "txtNombreFilter" },
        { data: "FechaEvento", ctroFilter: "txtNombreFilter" },
    ];
    SetDataTable("tblLogEventos", ServiceUrl + "Home/AjaxGetJsonDataLTLogEventos", dataColumns);
}

function Buscar() {
    $('#tblLogEventos').DataTable().search(
       JSON.stringify( {
            IdCCF: "1",
            IdUsuario: "2",
            Evento: "3",
            FechaEvento:"2018-01-01"
        })
    ).draw();
}
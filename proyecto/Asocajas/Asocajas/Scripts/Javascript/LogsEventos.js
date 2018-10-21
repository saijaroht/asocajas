$(document).ready(function () {
    ConsultarEventos();
    //BuscarTable("Buscartxt", "tbodyLogEventos");
});
var ListEventos = new Array();
function ConsultarEventos() {
    debugger;
    var dataColumns = [
        { data: "Evento", ctroFilter: "txtNombreFilter" },
        { data: "IdUsuario", ctroFilter: "txtNombreFilter" },
        { data: "FechaEvento", ctroFilter: "txtNombreFilter" },
    ];
    SetDataTable("tblLogEventos", ServiceUrl + "LTLogEventos/GetLTLogEventos", dataColumns);
    //if (ListEventos.length == 0) {
    //    consumirServicio(ServiceUrl + "LTLogEventos/GetLTLogEventos", null, function (data) {
    //        ListEventos = data;
    //        PrintTable();
    //    });
    //}
    //else {
    //    PrintTable();
    //}
}
//function PrintTable() {
//    debugger
//    $("#tbody").empty();
//    $.each(ListEventos, function (index, val) {
//        $("#tbody")
//           .append($("<tr />")
//               .append($("<td />", { html: val.Evento }))
//               .append($("<td />", { html: val.Usuario }))
//               .append($("<td />", { html: val.FechaEvento }))
//               .append($("<td />", { html: val.FechaFinEvento }))
               
//               );
//    });
//}
$(document).ready(function () {
    ConsultarExcepciones();
    //BuscarTable("Buscartxt", "tbodyLogEventos");
});
var ListExcepciones = new Array();
function ConsultarExcepciones() {
    debugger;
    if (ListExcepciones.length == 0) {
        consumirServicio(ServiceUrl + "LTLogConsultasAni/GetLTLogConsultasAni", null, function (data) {
            ListExcepciones = data;
            PrintTable();
        });
    }
    else {
        PrintTable();
    }
}
function PrintTable() {
    debugger
    $("#tbody").empty();
    $.each(ListExcepciones, function (index, val) {
        $("#tbody")
           .append($("<tr />")
                .append($("<td />", { html: val.Origen }))
                .append($("<td />", { html: val.FechaConsulta }))
                .append($("<td />", { html: val.FechaInicia }))
                .append($("<td />", { html: val.FechaFin }))
                .append($("<td />", { html: val.Duracion }))
                .append($("<td />", { html: val.IdUsuario }))
                
               
               

               );
    });
}
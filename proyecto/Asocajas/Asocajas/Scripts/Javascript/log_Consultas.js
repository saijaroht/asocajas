$(document).ready(function () {
    ConsultarConsultasAni();
    //BuscarTable("Buscartxt", "tbodyLogEventos");
});
var ListConsulta= new Array();
function ConsultarConsultasAni() {
    debugger;
    if (ListConsulta.length == 0) {
        consumirServicio(ServiceUrl + "LTLogConsultasAni/GetLTLogConsultasAni", null, function (data) {
            ListConsulta = data;
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
    $.each(ListConsulta, function (index, val) {
        $("#tbody")
                .append($("<tr />")
                .append($("<td />", { html: val.RCCF.Nombre }))
                .append($("<td />", { html: val.RUsuario.Usuario }))
                .append($("<td />", { html: val.IdConsulta }))
                .append($("<td />", { html: val.RRptaRnec.RptaRnec }))
                .append($("<td />", { html: val.FechaConsulta }))
                .append($("<td />", { html: val.origen }))
                .append($("<td />", { html: val.Duracion }))
                .append($("<td />", { html: val.FechaInicia + "--- " + val.FechaFin }))
                




               );
    });
}
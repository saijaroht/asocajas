$(document).ready(function () {
    ConsultarConsultasAni();
    //BuscarTable("Buscartxt", "tbodyLogEventos");
});

var ListEventos = new Array();
function ConsultarConsultasAni() {
    debugger;
    var dataColumns = [
        { data: "RUsuario.RCCF.Nombre", ctroFilter: "txtNombreFilter" },
        { data: "RUsuario.Nombres", ctroFilter: "txtNombreFilter" },
        { data: "IdConsulta", ctroFilter: "txtNombreFilter" },
        { data: "IdConsulta", ctroFilter: "txtNombreFilter" },
        { data: "IdConsulta", ctroFilter: "txtNombreFilter" },
        { data: "IdConsulta", ctroFilter: "txtNombreFilter" },
        { data: "IdConsulta", ctroFilter: "txtNombreFilter" },
        { data: "IdConsulta", ctroFilter: "txtNombreFilter" },
      
    ];
    SetDataTable("tblLogConsultasAni", ServiceUrl + "Home/AjaxGetJsonDataLTLogConsultasAni", dataColumns);
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

//var ListConsulta= new Array();
//function ConsultarConsultasAni() {
//    debugger;
//    if (ListConsulta.length == 0) {
//        consumirServicio(ServiceUrl + "LTLogConsultasAni/GetLTLogConsultasAni", null, function (data) {
//            ListConsulta = data;
//            PrintTable();
//        });
//    }
//    else {
//        PrintTable();
//    }

//function PrintTable() {
//    debugger
//    $("#tbody").empty();
//    $.each(ListConsulta, function (index, val) {
//        $("#tbody")
//                .append($("<tr />")
//                .append($("<td />", { html: val.RCCF.Nombre }))
//                .append($("<td />", { html: val.RUsuario.Usuario }))
//                .append($("<td />", { html: val.IdConsulta }))
//                .append($("<td />", { html: val.RRptaRnec.RptaRnec }))
//                .append($("<td />", { html: val.RRptaAsocajas.RptaAsocajas }))
//                .append($("<td />", { html: val.Origen }))
//                .append($("<td />", { html: val.Duracion }))
//                .append($("<td />", { html: val.FechaInicia + "--- " + val.FechaFin }))
                




//               );
//    });
//}
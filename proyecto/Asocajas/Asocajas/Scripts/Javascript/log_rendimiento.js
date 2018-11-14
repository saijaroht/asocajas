$(document).ready(function () {
    CargarFecha('txtFechaIncial', 'yy-mm-dd');
    CargarFecha('txtFechaFinal', 'yy-mm-dd');
    cargaAdicionales();
    ConsultarEventos();
    //BuscarTable("Buscartxt", "tbodyLogEventos");
});

function ConsultarEventos() {
    debugger;
    var dataColumns = [
		{ data: "Duracion", ctroFilter: "txtNombreFilter" },
        { data: "RUsuario.RCCF.Nombre", ctroFilter: "txtNombreFilter" },
        { data: "RUsuario.Nombres", ctroFilter: "txtNombreFilter" },
        { data: "IdConsulta", ctroFilter: "txtNombreFilter" },
        { data: "ROrigen.OrigenConsulta", ctroFilter: "txtNombreFilter" },
        { data: "RRptaAsocajas.RptaAsocajas", ctroFilter: "txtNombreFilter" },
		{ data: "FechaInicia", ctroFilter: "txtNombreFilter" },
		{ data: "FechaFin", ctroFilter: "txtNombreFilter" },
		{ data: "ControlRNEC", ctroFilter: "txtNombreFilter" },
		{ data: "DescrRNEC", ctroFilter: "txtNombreFilter" },
    ];
    SetDataTable("tblLogConsultas_Ani", ServiceUrl + "Home/AjaxGetJsonDataLTLogConsultasTiempoRes", dataColumns);
}

function Buscar() {
    $('#dvLogConsultas_Ani').show();
    $('#tblLogConsultas_Ani').DataTable().search(
       JSON.stringify({
           search: {
               Duracion: $('#txtDuracion').val(),
               FechaInicial: $("#txtFechaIncial").val(),
               FechaFinal: $("#txtFechaFinal").val()
           }
       })
    ).draw();
}

var CCFList = new Array();
var UsuariosList = new Array();
function cargaAdicionales() {
    consumirServicio(ServiceUrl + "RCCF/GetRCCF", null, function (data) {
        $("#cboCCF").empty();
        $("#cboCCF").append('<option value="">Todos</option>');
        CCFList = data;
        $.each(data, function (i, val) {
            $("#cboCCF").append('<option value = "' + val.IdCcf + '">' + val.Nombre + '</option>');
        });
    }, null, function (dataError) {

    });

    $("#cboCCF").change(function () {
        if ($(this).val() == "") {
            CargarUsuarios();
        } else {
            $("#cboUsuario").empty();
            var idCCF = $(this).val();
            var lista = Enumerable.From(UsuariosList)
            .Where(function (x) { return x.IdCcf == idCCF })
            .ToArray();
            $("#cboUsuario").append('<option value="">Todos</option>');
            $.each(lista, function (i, val) {
                $("#cboUsuario").append('<option value = "' + val.IdUsuario + '">' + val.Nombres + '</option>');
            });
        }
    });

    consumirServicio(ServiceUrl + "RUsuario/GetRUsuario", null, function (data) {
        UsuariosList = data;
        CargarUsuarios();
    }, null, function (dataError) {

    });
}

function CargarUsuarios() {
    $("#cboUsuario").empty();
    $("#cboUsuario").append('<option value="">Todos</option>');
    $.each(UsuariosList, function (i, val) {
        $("#cboUsuario").append('<option value = "' + val.IdUsuario + '">' + val.Nombres + '</option>');
    });
}
$(document).ready(function () {
    CargarUsuarios();
    ConsultarEventos();
});

function CargarUsuarios() {
    $("#cboUsuario").empty();
    $("#cboUsuario").append('<option value="">Todos</option>');
    consumirServicio(ServiceUrl + "RUsuario/GetRUsuarioByCCF", null, function (UsuariosList) {

        $.each(UsuariosList, function (i, val) {
            $("#cboUsuario").append('<option value = "' + val.IdUsuario + '">' + val.Nombres + '</option>');
        });
    }, null, function (dataError) {

    });
}

function ConsultarEventos() {
    debugger;
    var dataColumns = [
        //{ data: "RUsuario.RCCF.Nombre", ctroFilter: "txtNombreFilter" },
        { data: "RUsuario.Nombres", ctroFilter: "txtNombreFilter" },
        { data: "IdConsulta", ctroFilter: "txtNombreFilter" },
		{ data: "Nuip", ctroFilter: "txtNombreFilter" },
        { data: "ROrigen.OrigenConsulta", ctroFilter: "txtNombreFilter" },
        { data: "RRptaAsocajas.RptaAsocajas", ctroFilter: "txtNombreFilter" },
		{ data: "FechaInicia", ctroFilter: "txtNombreFilter" },
		{ data: "FechaFin", ctroFilter: "txtNombreFilter" },
		{ data: "ControlRNEC", ctroFilter: "txtNombreFilter" },
		{ data: "DescrRNEC", ctroFilter: "txtNombreFilter" },
    ];
    SetDataTable("tblLogConsultas_Ani", ServiceUrl + "Home/AjaxGetJsonDataLTLogConsultasAniByCCF", dataColumns);
}

function Buscar() {
    $('#dvLogConsultas_Ani').show();
    $('#tblLogConsultas_Ani').DataTable().search(
       JSON.stringify({
           search: {
               IdCCF: $('#cboCCF').val(),
               IdUsuario: $('#cboUsuario').val(),
               FechaInicial: $("#txtFechaIncial").val(),
               FechaFinal: $("#txtFechaFinal").val()
           }
       })
    ).draw();
}


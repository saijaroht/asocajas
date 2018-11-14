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
        { data: "Fecha", ctroFilter: "txtNombreFilter" },
        { data: "Lapso", ctroFilter: "txtNombreFilter" },
		{ data: "Cantidad", ctroFilter: "txtNombreFilter" },
    ];
    SetDataTable("tblConsultasProcesadas", ServiceUrl + "Home/AjaxGetJsonDataLTLogConsultasProcesadas", dataColumns);
}

function Buscar() {
    $('#dvConsultasProcesadas').show();
    $('#tblConsultasProcesadas').DataTable().search(
       JSON.stringify({
           search: {
               IdCCF: $('#cboCCF').val(),
               IdUsuario: $('#cboUsuario').val(),
               FechaInicial: $("#txtFechaIncial").val(),
               FechaFinal: $("#txtFechaFinal").val()
           }
       })
    ).draw();
    //$('#ThFecha').show();
    //$('#ThLapso').hide();

    if ($("#txtFechaIncial").val() != "" && $("#txtFechaFinal").val() != "") {
        if ($("#txtFechaIncial").val() == $("#txtFechaFinal").val()) {
            //$('#ThFecha').hide();
            //$('#ThLapso').show();

            $('#tblConsultasProcesadas').DataTable().columns(0).visible(false).columns(1).visible(true);
        } else {
            $('#tblConsultasProcesadas').DataTable().columns(0).visible(true).columns(1).visible(false);
        }
    } else
        $('#tblConsultasProcesadas').DataTable().columns(0).visible(true).columns(1).visible(false);

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
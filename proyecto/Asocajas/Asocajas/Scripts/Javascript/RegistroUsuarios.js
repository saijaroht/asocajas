$(document).ready(function () {
    //CargarFechaInicioFechaFin('txtFechadecaducidad');
    cargaAdicionales()


});
function cargaAdicionales()
{
    debugger;
    consumirServicio(ServiceUrl + "RCCF/GetRCCF", null, function (data) {
        $("#cboNombreCCF").append('<option value="0">Seleccione...</option>');
        $.each(data, function (i, val) {
            $("#cboNombreCCF").append('<option value = "' + val.IdCcf + '">' + val.Nombre + '</option>');
        });
    }, null, function (dataError) {
       
    });
}

function ValidaUsuario() {
    debugger;
    var campos = ["txtNombres","txtApellidos","txtUsuario","txtFechadecaducidad","cboEstado","cboTipodeusuario"];
    if (validarcampos(campos)) {
        GuardarUsuario();
    }
}
function GuardarUsuario() {

    var item = {
       
        Nombre: $('#txtNombre').val(),
        Apellido: $('#txtApellido').val(),
        Usuario: $('#txtUsuario').val(),
        Vigencia: $('#txtFechadecaducidad').val(),
        Estado: $('#cboEstado').val(),
        IdCcf: 1,
        IdRole: $('#cboTipodeusuario').val(),
    }

    consumirServicio(ServiceUrl + "RUsuario/PostRUsuario", item, function (data) {

    }, null, function (dataError) {
        alert("ingreso de forma satisfactoria");
    });
}
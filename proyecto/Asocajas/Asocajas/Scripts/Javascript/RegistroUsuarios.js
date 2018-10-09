$(document).ready(function () {
    CargarFechaInicioFechaFin('txtFechadecaducidad');
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
    consumirServicio(ServiceUrl + "RRole/GetRRole", null, function (data) {
        $("#cboTipodeusuario").append('<option value="0">Seleccione...</option>');
        $.each(data, function (i, val) {
            $("#cboTipodeusuario").append('<option value = "' + val.IdRole + '">' + val.Nombre + '</option>');
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
       
        Nombre: $('#txtNombres').val(),
        Apellido: $('#txtApellidos').val(),
        Usuario: $('#txtUsuario').val(),
        Vigencia: $('#txtFechadecaducidad').val(),
        Estado: 1,
        IdCcf: $('#cboTipodeusuario').val(),
        IdRole: $('#cboNombreCCF').val(),
    }

    SaveService(ServiceUrl + "RUsuario/PostRUsuario", item, function (data) {
        ShowMessage("NOTIFICACIÓN", "Se ingreso el Usuario de manera satisfactoria", "Alerta");
        window.location.href = "Gestion_Usuarios.aspx";

    }, null, function (dataError) {
    });
}
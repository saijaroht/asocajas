$(document).ready(function () {
    CargarFechaInicioFechaFin('txtFechadecaducidad');

});

function ValidaUsuario() {

    var campos = ["txtNombre,txtApellido,txtUsuario,txtFechadecaducidad,cboEstado,cboTipodeusuario"];
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
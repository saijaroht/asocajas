$(document).ready(function () {
    CargarFechaInicioFechaFin('txtFechadecaducidad');

});

function GuardarUsuario() {
    var item = {
        IdUsuario: 0,
        Nombre: $('#txtNombre').val(),
        Usuario: 0,
        Password: 0,
        Vigencia: 0,
        Estado: 0,
        IdCcf: 0,
        IdRole: 0,
    }

    consumirServicio(ServiceUrl + "RUsuario/PostRUsuario", item, function (data) {

    }, null, function (dataError) {

    });
}
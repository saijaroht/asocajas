$(document).ready(function () {

});
function ActualizarContraseña() {
    var campos = ["txtNuevaContraseña"];
    if (!validarcampos(campos)) {
        return;
    }
    //PostService(location.origin + '/Services/Servicios.aspx/IsLogin', null, function (data) {
        //if (data.Ok) {
            var item = {
                Password: $('#txtNuevaContraseña').val(),
                Usuario: ""
            }

            UpdateService(ServiceUrl + "RUsuario/PutUpdatePassword", item, function (data) {
                ShowMessage("NOTIFICACIÓN", "Su contraseña ha sido cambiada exitosamente. Por favor vuelva a ingresar al sistema Validador de clientes. <br> <a href='Login.aspx'>Ir a la página principal de ingreso</a>", "SoloMensaje");

            }, null, function (dataError) {
            });
    //    }
    //});
}
$(document).ready(function () {

});
function ActualizarContraseña() {
    var campos = ["txtNuevaContraseña"];

    if (!ValidarCtrlPass(document.getElementById("txtContrasenaActual")) || !ValidarCtrlPass(document.getElementById("txtNuevaContraseña"), true)) {
        return;
    } else if (!ValidarPasswordIguales(document.getElementById("txtNuevaContraseña"), document.getElementById("txtConfirmarContraseña"))) {
        return;
    }
    PostService(location.origin + '/Services/Servicios.aspx/IsLoginMail', null, function (data) {

        if (data.Ok) {
            consumirServicio(ServiceUrl + "RUsuario/GetExisteUser?user=" + data.Message + "&password=" + $("#txtContrasenaActual").val() + "", null, function (dataContrasenaActual) {
                if (dataContrasenaActual.Ok) {
                    var item = {
                        Password: $('#txtNuevaContraseña').val(),
                        Usuario: data.Message
                    }

                    UpdateService(ServiceUrl + "RUsuario/PutUpdatePassword", item, function (data) {
                        ShowMessage("NOTIFICACIÓN", "Su contraseña ha sido cambiada exitosamente. ", "SoloMensaje");
                        removerValidacion(["txtContrasenaActual", "txtNuevaContraseña", "txtConfirmarContraseña"], true);
                    });
                } else {
                    ShowMessage("NOTIFICACIÓN", dataContrasenaActual.Message, "SoloMensaje");
                }
            });

        }
    });
}
$(document).ready(function () {
    
});

function oprimirbtn() {

    var item = {
        Usuario: $('#txtContrasena').val(),
    }
    UpdateService(ServiceUrl + "RUsuario/PutRecordarContrasena", item, function (data) {
        debugger;
        //ShowMessage("NOTIFICACIÓN", "Su contraseña ha sido cambiada exitosamente. ", "SoloMensaje");
        //removerValidacion(["txtContrasenaActual", "txtNuevaContraseña", "txtConfirmarContraseña"], true);
        if (data.Message) {
            ShowMessage("NOTIFICACIÓN", data.Message, "SoloMensaje");
        } else {
            ShowMessage("NOTIFICACIÓN", "Hemos enviado las instrucciones de restablecimiento de la contraseña a su dirección de correo <br /> Si no recibe Instrucciones durante 1 minuto o 2 compruebe el spam y los filtros de correo basura de su correo electrónico o intente <a href='javascript:oprimirbtn();'>Reenviar Solicitud<a>", "SoloMensaje");
        }
    });
}
function cancelar() {
    window.location.href = "Login.aspx";
}
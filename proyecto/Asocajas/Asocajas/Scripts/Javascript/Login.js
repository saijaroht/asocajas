$(document).ready(function () {
    Logout();
});
var captchaComplete = true;
var recaptchaCallback = function () {
    debugger;
    console.log('recaptcha is ready'); // not showing
    grecaptcha.render("recaptcha", {
        sitekey: '6LcBs3QUAAAAAON6l4A3w66mtziMtI3YyoWGtECD',
        callback: function (response) {
            $.ajax({
                type: "POST",
                url: '../Services/Servicios.aspx/VerifyCaptcha',
                data: "{response: '" + response + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (r) {
                    var captchaResponse = jQuery.parseJSON(r.d);
                    if (captchaResponse.success) {
                        captchaComplete = true;
                        $('#lblvalidacioncaptchaok').show();
                        $('#lblvalidacioncaptcha').hide();
                    } else {
                        $('#txtcaptcha').val("");
                        var error = captchaResponse["error-codes"][0];
                        $('#lblvalidacioncaptcha').html("RECaptcha error. " + error);
                    }
                    debugger;
                    console.log('recaptcha callback');
                },
                error: function (data) {

                    console.log(data);

                }
            });
        }
    });
}
function ValidaUsuario()
{
    var campos = ["txtUsuario", "txtContrasena"];
    if (!validarcampos(campos) || !captchaComplete) {
        if (!captchaComplete)
            $('#lblvalidacioncaptcha').show();
        return;
    }
    debugger;
    consumirServicio(ServiceUrl + "RUsuario/GetExistUser?user=" + $("#txtUsuario").val() + "&password=" + $("#txtContrasena").val()+"", null, function (data) {
        if (!data.Ok) {
            ShowMessage("NOTIFICACIÓN", data.Message, "SoloMensaje");
        } else {
            //SessionLogin($("#txtUsuario").val(), function (dataUser) {
                if (data.CambioObligatorio)
                    window.location.href = "CambioObligatorioClave.aspx";
                else
                    window.location.href = "AllPages/Inicio.aspx";
            //});
        }
    }, null, function (dataError) { });
}
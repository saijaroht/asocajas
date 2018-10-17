$(document).ready(function () {
    Logout();
});

var recaptchaCallback = function () {
    debugger;
    console.log('recaptcha is ready'); // not showing
    grecaptcha.render("recaptcha", {
        sitekey: '6Lc9FXQUAAAAAAD9ZwF-PjD0e10HdtZDnJJQPFp0',
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
                        $('#lblvalidacioncaptchaok').show();
                        $('#lblvalidacioncaptcha').hide();
                    } else {
                        $('#txtcaptcha').val("");
                        var error = captchaResponse["error-codes"][0];
                        $('#lblvalidacioncaptcha').html("RECaptcha error. " + error);
                    }
                    debugger;
                    console.log('recaptcha callback');
                }
            });
        }
    });
}
function ValidaUsuario()
{
    debugger;
    consumirServicio(ServiceUrl + "RUsuario/GetExistUser?user=" + $("#txtUsuario").val() + "&password=" + $("#txtContrasena").val()+"", null, function (data) {
        if (!data.Ok) {
            ShowMessage("NOTIFICACIÓN", data.Message, "SoloMensaje");
        } else {
            SessionLogin(data.Message, function (dataUser) {
                if (data.CambioObligatorio)
                    window.location.href = "CambioObligatorioClave.aspx";
                else
                    window.location.href = "AllPages/Inicio.aspx";
            });
        }
    }, null, function (dataError) { });
}
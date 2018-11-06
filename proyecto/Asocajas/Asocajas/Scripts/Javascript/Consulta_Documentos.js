$(document).ready(function () {
    consultaDocumento();
    debugger;
    
});

function consultaDocumento()
{
    debugger;
    consumirServicio(ServiceUrl + "Home/GetCedula?Cedula=1000397409", null, function (dataUsuario) {
        
        if (dataUsuario.datosCedulas.nuip)
        { abrirModal(); }
        else
        { alerta();}
    });
    
}

    function alerta() {
        ShowMessage("Mensaje Sistema", "El número de documento no existe en el sistema", "Alerta");

    }
    function abrirModal() {
        var Consultar = $("#btnConsultar");
        Consultar.click(function () {
            $('#ModalConsultarUsuarioDocumento').modal('show');
        })
    }





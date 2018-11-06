$(document).ready(function () {

    
});

function alerta() {
    ShowMessage("Mensaje Sistema", "El número de documento no existe en el sistema", "Alerta");

}
function abrirModal() {
    var Consultar = $("#btnConsultar");
    Consultar.click(function () {
        $('#ModalConsultarUsuarioDocumento').modal('show');
    })
}





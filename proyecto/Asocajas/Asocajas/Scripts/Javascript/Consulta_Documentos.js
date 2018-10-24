$(document).ready(function () {

    abrirModal();
});

function abrirModal() {
    var Consultar = $("#btnConsultar");
    Consultar.click(function () {
        $('#ModalConsultarUsuarioDocumento').modal('show');
    })
}





$(document).ready(function () {
  
});

function consultaDocumento()
{
    debugger;

    var ListaData;
    consumirServicio(ServiceUrl + "Home/GetCedula?Cedula="+ $('#Identificacion').val(), null, function (dataUsuario) {
        ListaData=dataUsuario;
        if (dataUsuario.datosCedulas.nuip)
        { abrirModal(ListaData); }
        else
        { alerta(); }
        
    });
    
        
     

    
}

    function alerta() {
        ShowMessage("Mensaje Sistema", "El número de documento no existe en el sistema", "Alerta");

    }
    function abrirModal(ListaData) {
        
       
        $('#txtanoResolucion').val(ListaData.datosCedulas.anoResolucion);
        //$('#txcodError').val(ListaData.datosCedulas.codError);
        $('#txtdepartamentoExpedicion').val(ListaData.datosCedulas.departamentoExpedicion);
        //$('#txdescripcionError').val(ListaData.datosCedulas.descripcionError);
        $('#txtestadoCedula').val(ListaData.datosCedulas.estadoCedula);
        $('#txtfechaDefuncion').val(ListaData.datosCedulas.fechaDefuncion);
        $('#txtfechaExpedicion').val(ListaData.datosCedulas.fechaExpedicion);
       // $('#txtfechaHoraConsulta').val(ListaData.datosCedulas.fechaHoraConsulta);
        $('#txtfechaNacimiento').val(ListaData.datosCedulas.fechaNacimiento);
        $('#txtgenero').val(ListaData.datosCedulas.genero);
       // $('#txtinformante').val(ListaData.datosCedulas.informante);
        $('#txtlugarNacimiento').val(ListaData.datosCedulas.lugarNacimiento);
        $('#txtlugarNovedad').val(ListaData.datosCedulas.lugarNovedad);
        $('#txtlugarPreparacion').val(ListaData.datosCedulas.lugarPreparacion);
        $('#txtmunicipioExpedicion').val(ListaData.datosCedulas.municipioExpedicion);
        $('#txtnuip').val(ListaData.datosCedulas.nuip);
        $('#txtnumResolucion').val(ListaData.datosCedulas.numResolucion);
        $('#txtnumeroControl').val(ListaData.datosCedulas.numeroControl);
        $('#txtparticula').val(ListaData.datosCedulas.particula);
        $('#txtprimerApellido').val(ListaData.datosCedulas.primerApellido);
        $('#txtprimerNombre').val(ListaData.datosCedulas.primerNombre);
        $('#txtsegundoApellido').val(ListaData.datosCedulas.segundoApellido);
        $('#txtsegundoNombre').val(ListaData.datosCedulas.segundoNombre);
        // $('#serial').val(ListaData.datosCedulas.serial);
        $('#ModalConsultarUsuarioDocumento').modal('show');
        
    }





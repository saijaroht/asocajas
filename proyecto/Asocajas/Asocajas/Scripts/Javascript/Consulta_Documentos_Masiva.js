$(document).ready(function () {
    
   
})

function BuscarData() {

    leerArchivo(function (data) {
        debugger;
        data =   data.split(/\r?\n|\r/);
        item = {
            Cedulas: data
        }
        SaveService(ServiceUrl + "Home/PostListCedulas", item, function (dataUsuario) {
            debugger;

            $.each(dataUsuario, function (index, val) {

                debugger;

                
                $("#dvDatos").append(
                    $(('<tr />'))
                    .append($("<td />", { html: val.datosCedulas.nuip }))
                    .append($("<td />", { html: val.datosCedulas.particula }))
                    .append($("<td />", { html: val.datosCedulas.primerApellido }))
                    .append($("<td />", { html: val.datosCedulas.segundoApellido }))
                    .append($("<td />", { html: val.datosCedulas.primerNombre }))
                    .append($("<td />", { html: val.datosCedulas.segundoNombre }))
                    .append($("<td />", { html: val.datosCedulas.fechaNacimiento }))
                     .append($("<td />", { html: val.datosCedulas.municipioExpedicion }))
                      .append($("<td />", { html: val.datosCedulas.departamentoExpedicion }))
                       .append($("<td />", { html: val.datosCedulas.fechaExpedicion }))
                        .append($("<td />", { html: val.datosCedulas.estadoCedula }))
                        .append($("<td />", { html: val.datosCedulas.numResolucion }))
                        .append($("<td />", { html: val.datosCedulas.anoResolucion }))
                    );
            });
            
           
            $('#MyModalConsultaMasiva').modal('show');
            pintarTabla();


        });
    });
}

function leerArchivo(functionSucces) {
    debugger;
    var archivo = $("#inputFile").get(0).files[0];
    //var archivo = e.target.files[0];
    if (!archivo) {
        return;
    }
    var lector = new FileReader();
    lector.onload = function (e) {
        var contenido = e.target.result;
        if (functionSucces)
            functionSucces(contenido);
    };
    lector.readAsText(archivo);
}


function pintarTabla() {
    $('#example').DataTable({
        "scrollY": "400px",
        "scrollCollapse": true,
        "pagingType": "full_numbers",
        language: {

            "decimal": "",
            "emptyTable": "No hay datos",
            "info": "Mostrando _START_ a _END_ de _TOTAL_ registros",
            "infoEmpty": "Mostrando 0 a 0 de 0 registros",
            "infoFiltered": "(Filtro de _MAX_ total registros)",
            "infoPostFix": "",
            "thousands": ",",
            "lengthMenu": "Mostrar _MENU_ registros",
            "loadingRecords": "Cargando...",
            "processing": "Procesando...",
            "search": "Buscar:",
            "zeroRecords": "No se encontraron coincidencias",
            "paginate": {
                "first": "Primero",
                "last": "Ultimo",
                "next": "Próximo",
                "previous": "Anterior"
            },
            "aria": {
                "sortAscending": ": Activar orden de columna ascendente",
                "sortDescending": ": Activar orden de columna desendente"
            }

        }
    });
}

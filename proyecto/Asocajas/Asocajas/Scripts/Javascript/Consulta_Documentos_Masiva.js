$(document).ready(function () {
    debugger
   
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
            //ListaData = dataUsuario;
            //if (dataUsuario.datosCedulas.nuip)
            //{ abrirModal(ListaData); }
            //else
            //{ alerta(); }

        });
    });
    //var files = $("#inputFile").get(0).files;
    //var data = new FormData();
    //for (i = 0; i < files.length; i++) {
    //    data.append("file" + i, files[i]);
    //}
    //$.ajax({
    //    type: "POST",
    //    url: "/api/file",
    //    contentType: false,
    //    processData: false,
    //    data: data,
    //    success: function (result) {
    //        if (result) {
    //            alert('Archivos subidos correctamente');
    //            $("#inputFile").val('');
    //        }
    //    }
    //});
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


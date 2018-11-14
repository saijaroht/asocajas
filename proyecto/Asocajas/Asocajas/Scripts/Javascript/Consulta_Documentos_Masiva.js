$(document).ready(function () {
    debugger
   
})

function BuscarData()
{

   
        var files = $("#inputFile").get(0).files;
        var data = new FormData();
        for (i = 0; i < files.length; i++) {
            data.append("file" + i, files[i]);
        }
        $.ajax({
            type: "POST",
            url: "/api/file",
            contentType: false,
            processData: false,
            data: data,
            success: function (result) {
                if (result) {
                    alert('Archivos subidos correctamente');
                    $("#inputFile").val('');
                }
            }
        });

}
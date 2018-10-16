$(document).ready(function () {
    ConsultarUsuarios();
    Buscador()
});

function nuevoUsuario() {
    window.location.href = "Registro_Usuarios.aspx";
}

function ConsultarUsuarios() {
    debugger;
    consumirServicio(ServiceUrl + "RUsuario/GetRUsuario", null, function (data) {
        var datos = data;
        $.each(datos, function (index, val) {
          
            var btnEditar = $('<p />', { 'data-placement': "top", 'data-toggle': "tooltip", title: "Editar" })
                            .append($("<button />", { class: "btn btn-primary btn-xs" })
                            .append($('<span />', { class: "glyphicon glyphicon-pencil" })));

            var btnEliminar = $('<p />', { 'data-placement': "top", 'data-toggle': "tooltip", title: "Eliminar" })
                            .append($("<button />", { class: "btn btn-danger btn-xs" })
                            .append($('<span />', { class: "glyphicon glyphicon-trash" })));

            var btnVer = $('<p />', { 'data-placement': "top", 'data-toggle': "tooltip", title: "Abrir" })
                            .append($("<button />", { class: "btn btn-success btn-xs" })
                            .append($('<span />', { class: "glyphicon glyphicon-eye-open" })));

            var btnBlock = $('<p />', { 'data-placement': "top", 'data-toggle': "tooltip", title: "Cerrar" })
                           .append($("<button />", { class: "btn btn-danger btn-xs" })
                           .append($('<span />', { class: "glyphicon glyphicon-eye-close" })));

            var tipoBtn = val.Estado == 1 ? btnVer : btnBlock;
                                        

            //var btn = $('<p />',{'data-placement':"top", 'data-toggle':"tooltip", title:"Edit"}))
            //            .append($('<span />',{class:"glyphicon glyphicon-pencil"})));
            var check = $('<input/>',{"type": "checkbox", class:"checkthis"});
            $("#tbodyGestionUsuarios")
                .append($("<tr />")
                    .append($("<td />").append(check))
                    .append($("<td />", { html: val.Nombre }))
                    .append($("<td />", { html: val.Apellido }))
                    .append($("<td />", { html: val.Usuario }))
                    .append($("<td />", { html: val.RCCF.Nombre }))
                    .append($("<td />", { html: val.RRole.Nombre }))
                    .append($("<td />", { html: val.Estado }))
                    .append($("<td />").append(btnEditar))
                    .append($("<td />").append(btnEliminar))
                    .append($("<td />").append(tipoBt n))

                    );

        });
    });
}


function Buscador() {
    $("#myInput").on("keyup", function () {
        var value = $(this).val().toLowerCase();
            $("#tbodyGestionUsuarios tr").filter(function () {
            $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
        });
    });
}

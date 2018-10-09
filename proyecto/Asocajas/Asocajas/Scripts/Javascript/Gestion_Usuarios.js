$(document).ready(function () {
    ConsultarUsuarios();
});

function ConsultarUsuarios() {
    debugger;
    consumirServicio(ServiceUrl + "RUsuario/GetRUsuario", null, function (data) {
        var datos = data;
        $.each(datos, function (index, val) {
            //<p ><button><span ></span></button></p>

            var btn = $('<p />',{'data-placement':"top", 'data-toggle':"tooltip", title:"Edit"}
                        .append($('<button />',{class:"btn btn-primary btn-xs"}))
                        .append($('<span />',{class:"glyphicon glyphicon-pencil"})));
            var check = $('<input/>',{"type": "checkbox", class:"checkthis"});
            $("#tbodyGestionUsuarios")
                .append($("<tr />")
                    .append($("<td />").append(check))
                    .append($("<td />", { html: val.Nombre }))
                    .append($("<td />", { html: val.Apellido }))
                    .append($("<td />", { html: val.Usuario }))
                    .append($("<td />", { html: val.IdCcf }))
                    .append($("<td />", { html: val.IdRole }))
                    .append($("<td />", { html: val.Estado }))
                    .append($("<td />", { html: val.Estado }))

                    );

        });
    });
}


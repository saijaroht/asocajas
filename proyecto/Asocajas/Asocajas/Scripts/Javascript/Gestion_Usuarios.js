$(document).ready(function () {
    CargarFechaInicioFechaFin('txtFechadecaducidadActualizar');
    ConsultarUsuarios();
<<<<<<< HEAD
    Buscador()
=======
    BuscarTable("Buscartxt", "tbodyGestionUsuarios");

    consumirServicio(ServiceUrl + "RRole/GetRRole", null, function (data) {
        $("#cboTipodeusuarioActualizar").append('<option value="0">Seleccione...</option>');
        $.each(data, function (i, val) {
            $("#cboTipodeusuarioActualizar").append('<option value = "' + val.IdRole + '">' + val.Nombre + '</option>');
        });
    }, null, function (dataError) {

    });
>>>>>>> 89a66e991518ef22369fd6de49261e2aef56e2bf
});

function nuevoUsuario() {
    window.location.href = "Registro_Usuarios.aspx";
}
var ListUsuarios = new Array();
function ConsultarUsuarios() {
    debugger;
<<<<<<< HEAD
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

=======
    if (ListUsuarios.length == 0) {
        consumirServicio(ServiceUrl + "RUsuario/GetRUsuario", null, function (data) {
            ListUsuarios = data;
            PrintTable();
>>>>>>> 89a66e991518ef22369fd6de49261e2aef56e2bf
        });
    }
    else {
        PrintTable();
    }
}

<<<<<<< HEAD

function Buscador() {
    $("#myInput").on("keyup", function () {
        var value = $(this).val().toLowerCase();
            $("#tbodyGestionUsuarios tr").filter(function () {
            $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
        });
    });
}
=======
function PrintTable() {
    $("#tbodyGestionUsuarios").empty();
    $.each(ListUsuarios, function (index, val) {

        var btnEditar = $('<p />', { 'data-placement': "top", 'data-toggle': "tooltip", title: "Editar" })
                        .append($("<button />", { class: "btn btn-primary btn-xs" })
                        .append($('<span />', { class: "glyphicon glyphicon-pencil" })));
        btnEditar.click(function () {
            $('#ModalEditar').modal('show');
            return false;
        });
        var btnEliminar = $('<p />', { 'data-placement': "top", 'data-toggle': "tooltip", title: "Eliminar" })
                        .append($("<button />", { class: "btn btn-danger btn-xs" })
                        .append($('<span />', { class: "glyphicon glyphicon-trash" })));

        var btnVer = $("<button />", { class: "btn btn-success btn-xs" }).append($('<span />', { class: "glyphicon glyphicon-eye-open" }));
        var btnBlock = $("<button />", { class: "btn btn-danger btn-xs" }).append($('<span />', { class: "glyphicon glyphicon-eye-close" }))

        var tipoBtn = $('<p />', { 'data-placement': "top", 'data-toggle': "tooltip" });
        if (val.Estado == 1) {
            tipoBtn.append(btnVer);
        } else {
            tipoBtn.append(btnBlock);
        }
        //var tipoBtn = val.Estado == 1 ? btnVer : btnBlock;
        var EstadoSTR = $("<td />", { html: val.EstadoSTR });
        tipoBtn.click({ _id: val.IdUsuario, _btnVer: btnVer, _btnBlock: btnBlock, _tipoBtn: tipoBtn, _EstadoSTR: EstadoSTR }, function (event) {
            debugger;
            Enumerable.From(ListUsuarios)
            .Where(function (x) { return x.IdUsuario == event.data._id })
            .FirstOrDefault().Estado = Enumerable.From(ListUsuarios)
            .Where(function (x) { return x.IdUsuario == event.data._id })
            .FirstOrDefault().Estado == "1" ? "2" : "1";
            //PrintTable();
            event.data._tipoBtn.empty();
            var estado = Enumerable.From(ListUsuarios)
            .Where(function (x) { return x.IdUsuario == event.data._id })
            .FirstOrDefault().Estado;
            if (estado == "1") {
                event.data._tipoBtn.append(event.data._btnVer);
                event.data._EstadoSTR.html("Activo");
            } else {
                event.data._tipoBtn.append(event.data._btnBlock);
                event.data._EstadoSTR.html("Bloqueado");
            }
            var item = {
                Estado: estado,
                IdUsuario: event.data._id,
            }
            UpdateService(ServiceUrl + "RUsuario/PutUpdateActivarBloquear", item, function (data) {
                //ShowMessage("NOTIFICACIÓN", "Su contraseña ha sido cambiada exitosamente. ", "SoloMensaje");
                //removerValidacion(["txtContrasenaActual", "txtNuevaContraseña", "txtConfirmarContraseña"], true);
            });
            return false;
        });

        //var btn = $('<p />',{'data-placement':"top", 'data-toggle':"tooltip", title:"Edit"}))
        //            .append($('<span />',{class:"glyphicon glyphicon-pencil"})));
        var check = $('<input/>', { "type": "checkbox", class: "checkthis" });
        $("#tbodyGestionUsuarios")
            .append($("<tr />")
                .append($("<td />").append(check))
                .append($("<td />", { html: val.Nombre }))
                .append($("<td />", { html: val.Apellido }))
                .append($("<td />", { html: val.Usuario }))
                .append($("<td />", { html: val.RCCF.Nombre }))
                .append($("<td />", { html: val.RRole.Nombre }))
                .append(EstadoSTR)
                .append($("<td />").append(btnEditar))
                //.append($("<td />").append(btnEliminar))
                .append($("<td />").append(tipoBtn))

                );

    });
}
>>>>>>> 89a66e991518ef22369fd6de49261e2aef56e2bf

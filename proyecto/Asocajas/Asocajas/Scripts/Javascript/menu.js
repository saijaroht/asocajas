var listaMenuPapa = new Array();
var listaMenuHijo = new Array();
$(document).ready(function () {
    ConsultarConsultasMenu();
    
    //BuscarTable("Buscartxt", "tbodyLogEventos");
})
function ConsultarConsultasMenu() {
    debugger;

    PostService(location.origin + '/Services/Servicios.aspx/IsLogin', null, function (data1) {
        if (data1.Ok) {
            var UsuarioAct = data1.Message;
            consumirServicio(ServiceUrl + "RMenu/GetRMenu?Usuario=" + UsuarioAct, null, function (data2) {
                var UbicacionActual = location.pathname.split("/")[location.pathname.split("/").length - 1];
                var Existe = Enumerable.From(data2)
                      .Where(function (x) { return x.url == UbicacionActual })
                       .Count() > 0 || UbicacionActual == "Inicio.aspx" ? true : false;
                if (Existe) {
                    listaMenuPapa = Enumerable.From(data2)
                       .Where(function (x) { return x.IdMenuHijo == 0 })
                         .OrderBy(function (x) { return x.Orden })
                        .ToArray();
                    listaMenuHijo = Enumerable.From(data2)
                       .Where(function (x) { return x.IdMenuHijo != 0 })
                         .OrderBy(function (x) { return x.Orden })
                        .ToArray();
                    $.each(listaMenuPapa, function (i, val) {
                        var hijos
                        listaMenuHijo1 = Enumerable.From(listaMenuHijo)
                           .Where(function (x) { return x.IdMenuHijo == val.IdMenu })
                             .OrderBy(function (x) { return x.Orden })
                            .ToArray();
                        $.each(listaMenuHijo1, function (i, val2) {
                            if (i == 0) {
                                hijos = "<li  class='menu-item'><a  href=" + val2.url + ">" + val2.Descripcion + "</a></li>";
                            }
                            else {
                                hijos = hijos + "<li  class='menu-item'><a  href=" + val2.url + ">" + val2.Descripcion + "</a></li>";
                            }
                        });
                        $("#menu")
                            .append($("<li />", { class: "menu-item dropdown" })
                            .append($("<a />", { href: "#", class: "dropdown-toggle", "data-toggle": "dropdown", html: val.Descripcion })
                            .append($("<b />", { class: "caret" })))
                            .append($("<ul />", { class: "dropdown-menu" }).append(hijos)));
                    });
                } else {
                    window.location.href = location.origin + "/Pages/AllPages/Inicio.aspx"
                }
            });
        }
    });

}

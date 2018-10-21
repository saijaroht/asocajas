var listaMenuPapa = new Array();
var listaMenuHijo = new Array();
$(document).ready(function () {
    ConsultarConsultasMenu();
    
    //BuscarTable("Buscartxt", "tbodyLogEventos");
})
function ConsultarConsultasMenu()
{
    debugger;
   
    PostService(location.origin + '/Services/Servicios.aspx/IsLogin', null, function (data1) {
        var UsuarioAct = data1.Message;
        consumirServicio(ServiceUrl + "RMenu/GetRMenu?Usuario=" + UsuarioAct, null, function (data2) {

            listaMenuPapa=Enumerable.From(data2)
               .Where(function (x) { return x.IdMenuHijo == 0 })
                 .OrderBy(function (x) { return x.Orden })
                .ToArray();
            listaMenuHijo = Enumerable.From(data2)
               .Where(function (x) { return x.IdMenuHijo != 0 })
                 .OrderBy(function (x) { return x.Orden })
                .ToArray();
        });
        $.each(listaMenuPapa, function (i, val) {
            var hijos
            debugger;
            listaMenuHijo1 = Enumerable.From(listaMenuHijo)
               .Where(function (x) { return x.IdMenuHijo == val.IdMenu })
                 .OrderBy(function (x) { return x.Orden })
                .ToArray();
            $.each(listaMenuHijo1, function (i, val2) {
                if (i == 0)
                { hijos = $("<li />", { class: "menu-item" }).append($("<a />", { href: val2.url, html: val2.Descripcion })); }
                else {
                    hijos = hijos.append( $("<li />", { class: "menu-item" }).append($("<a />", { href: val2.url, html: val2.Descripcion })));
                }
            });
            $("#menu")
                .append($("<li />", { class: "menu-item dropdown" })
                .append($("<a />", { href: "#", class: "dropdown-toggle", "data-toggle": "dropdown", html: val.Descripcion })
                .append($("<b />", { class: "caret" })))
                .append($("<ul />", { class: "dropdown-menu" }).append($(hijos))))
        });
    });
   
}

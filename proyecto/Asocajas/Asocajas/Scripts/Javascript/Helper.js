﻿var ServiceUrl = location.origin + '/api/';

var consumirServicio = function (direccion, datos, funcionexitosa, datosAdicionales, funcionError) {
    // debugger
    $.ajax({
        url: direccion,
        contentType: "application/json;odata=verbose",
        data: datos,
        headers: { "accept": "application/json;odata=verbose" },
        async: false,
        dataType: 'json',
        success: function (data) {
            if (funcionexitosa) {
                funcionexitosa(data, datosAdicionales);
            }
            // user = data.d.Title;
        },
        error: function (data) {
            if (funcionError) {
                funcionError(data);
            }
            // console.log("Failed to get customer");
        }
    });

    return false;
}

///traer la fecha actual
function hoyFecha() {
    var hoy = new Date();
    var dd = hoy.getDate();
    var mm = hoy.getMonth() + 1;
    var yyyy = hoy.getFullYear();

    dd = addZero(dd);
    mm = addZero(mm);

    return yyyy + '/' + mm + '/' + dd;
}
function addZero(i) {
    if (i < 10) {
        i = '0' + i;
    }
    return i;
}


//Método genérico para guardar
function SaveService(Url, objData, funcionSuccess, funcionError, datosAdicionales) {
    try {
        //
        var date = new Date();
        var fechaCreacion = (date.getFullYear() + "/" + (date.getMonth() + 1) + "/" + date.getDate() + " " + date.getHours() + ":" + date.getMinutes() + ":" + date.getSeconds());
        objData.FechaCreacion = fechaCreacion;
        objData.UsuarioCreacion = '1';
        objData.MaquinaCreacion = '1';
        objData.Activo = true;

        $.ajax({
            type: "POST",
            dataType: "json",
            url: Url,
            data: objData,
            success: function (data) {
                if (funcionSuccess) {
                    funcionSuccess(data, datosAdicionales);
                }
            },
            error: function (data) {
                if (funcionError) {
                    funcionError(data);
                } else {
                    console.log(objData);
                    console.log(data);
                }
            }
        });

    }
    catch (ex) {
        console.log(ex.message);
    }
}

//Método genérico para guardar
function UpdateService(Url, objData, funcionSuccess, funcionError, datosAdicionales) {
    try {

        $.ajax({
            type: "PUT",
            dataType: "json",
            url: Url,
            data: objData,
            success: function (data) {
                if (funcionSuccess) {
                    funcionSuccess(data, datosAdicionales);
                }
            },
            error: function (data) {
                if (funcionError) {
                    funcionError(data);
                } else {
                    console.log(objData);
                    console.log(data);
                }
            }
        });

    }
    catch (ex) {
        console.log(ex.message);
    }
}

function validarcampos(arraycampos) {
    //debugger
    var validacionregistro = true;
    $.each(arraycampos, function (index, value) {

        if (document.getElementsByName(value).length > 0) {
            $.each(document.getElementsByName(value), function (index, valueFile) {

                if (!validateText(valueFile)) {
                    if (validacionregistro) {
                        valueFile.focus();
                    }
                    validacionregistro = false;
                }

            })
            //debugger;

        }
        else
            if (!validateText(document.getElementById(value))) {
                if (validacionregistro) {
                    document.getElementById(value).focus();
                }
                validacionregistro = false;
            }
    });
    return validacionregistro;
}

function AddTextErrorToInput(control, MensajeError) {
    if (control.id != "") {
        if ($(control).parent()[0].nodeName == "TD")
            div = $(control).closest("TD");
        else
            div = $(control).closest("div");

        div.addClass("has-error has-feedback");
        div.append('<span id="glypcn' + control.id + '" class="glyphicon glyphicon-remove form-control-feedback "></span>')
        $("#glypcnNovalid" + control.id).remove();
        div.append('<span id="glypcnNovalid' + control.id + '" class="help-block">' + MensajeError + '</span>');
        return false;
    }
}
function validateText(control, validPass) {
    if (control.id != "") {
        if ($(control).parent()[0].nodeName == "TD")
            div = $(control).closest("TD");
        else
            div = $(control).closest("div");

        // var typecontrol=$(control).attr("type").toLowerCase();
        switch (control.type) {
            case "text":
            case "number":

            case "textarea":
                if ($("#" + control.id).val() == null || $("#" + control.id).val() == "") {
                    div.removeClass("has-success");
                    $("#glypcn" + control.id).remove();
                    div.addClass("has-error has-feedback");
                    div.append('<span id="glypcn' + control.id + '" class="glyphicon glyphicon-remove form-control-feedback "></span>');
                    return false;
                }
                else {
                    div.removeClass("has-error");
                    div.addClass("has-success has-feedback");
                    $("#glypcn" + control.id).remove();
                    div.append('<span id="glypcn' + control.id + '" class="glyphicon glyphicon-ok form-control-feedback "></span>');
                    return true;
                }
                break;
            case "file":
                if ($("#" + control.id).val() == null || $("#" + control.id).val() == "") {
                    div.removeClass("has-success");
                    $("#glypcn" + control.id).remove();
                    div.addClass("has-error has-feedback");
                    div.append('<span id="glypcn' + control.id + '" class="glyphicon glyphicon-remove form-control-feedback "></span>');
                    return false;
                }
                else {
                    div.removeClass("has-error");
                    div.addClass("has-success has-feedback");
                    $("#glypcn" + control.id).remove();
                    div.append('<span id="glypcn' + control.id + '" class="glyphicon glyphicon-ok form-control-feedback "></span>');

                    $("#glypcnNovalid" + control.id).remove();
                    div.append('<span id="glypcn' + control.id + '" class="glyphicon glyphicon-ok form-control-feedback"></span>');

                    var EsIgualValue = false;
                    var FilesNamesArray = new Array();
                    $("input[name='" + control.name + "']").each(function (index, value) {
                        if (value.files.length > 0)
                            FilesNamesArray.push(value.files[0].name);
                    });
                    var CountEquals = Enumerable.From(FilesNamesArray)
                        .Where(function (x) { return x == control.files[0].name })
                        .Count();
                    if (CountEquals > 1) {
                        div.addClass("has-error has-feedback");
                        div.append('<span id="glypcn' + control.id + '" class="glyphicon glyphicon-remove form-control-feedback "></span>')
                        $("#glypcnNovalid" + control.id).remove();
                        div.append('<span id="glypcnNovalid' + control.id + '" class="help-block">' + "Hay " + CountEquals + " archivos con el nombre: " + control.files[0].name + '</span>');
                        return false;
                    }
                    else {
                        return true;
                    }
                }
                break;
            case "radio":
                //
                var eschecked = false;
                $("input[name='" + control.name + "']").each(function (index, value) {
                    //
                    if ($(value).is(":checked")) {
                        eschecked = true;
                    }
                });
                if (!eschecked) {
                    div.removeClass("has-success");
                    $("#glypcn" + control.id).remove();
                    div.addClass("has-error has-feedback");
                    div.append('<span id="glypcn' + control.id + '" class="glyphicon glyphicon-remove form-control-feedback "></span>');
                    return false;
                }
                else {
                    div.removeClass("has-error");
                    div.addClass("has-success has-feedback");
                    $("#glypcn" + control.id).remove();
                    div.append('<span id="glypcn' + control.id + '" class="glyphicon glyphicon-ok form-control-feedback "></span>');
                    return true;
                }
                break;
            case "password":
                RemoveValidateText(control);
                if ($("#" + control.id).val() == null || $("#" + control.id).val() == "") {
                    div.removeClass("has-success");
                    $("#glypcn" + control.id).remove();
                    div.addClass("has-error has-feedback");
                    div.append('<span id="glypcn' + control.id + '" class="glyphicon glyphicon-remove form-control-feedback "></span>');
                    return false;
                }
                else {
                    var Passwords = document.querySelectorAll('input[type=password]');
                    if (Passwords.length != 2) {
                        var valueInput = $("#" + control.id).val();
                        //var lowerCaseLetters = /[a-z]/g;
                        //var upperCaseLetters = /[A-Z]/g;
                        //var numbers = /[0-9]/g;
                        //var errorText = true;
                        //if (!valueInput.match(lowerCaseLetters) || !valueInput.match(upperCaseLetters) || !valueInput.match(numbers) || !valueInput.match(numbers) || !(valueInput.length >= 8)) {
                        //    errorText = false;
                        //}

                        var regex = /^(?=.*\d)(?=.*[a-záéíóúüñ])(?=.*[A-ZÁÉÍÓÚÜÑ])(?=.*[()\/&%$#.@])/;
                        var errorRegex = !regex.test($("#" + control.id).val()) || !($("#" + control.id).val().length >= 8) ? false : true;

                        if ($("#" + control.id).val() == null || $("#" + control.id).val() == "" || (!errorRegex && validPass)) {
                            div.removeClass("has-success");
                            $("#glypcn" + control.id).remove();
                            div.addClass("has-error has-feedback");
                            div.append('<span id="glypcn' + control.id + '" class="glyphicon glyphicon-remove form-control-feedback "></span>');
                            //AddTextErrorToInput(control, "La contraseña debe tener ");
                            
                            return false;
                        }
                        else {
                            div.removeClass("has-error");
                            div.addClass("has-success has-feedback");
                            $("#glypcn" + control.id).remove();
                            div.append('<span id="glypcn' + control.id + '" class="glyphicon glyphicon-ok form-control-feedback "></span>');
                            return true;
                        }
                    }
                    else {
                        debugger;
                        var regex = /^(?=.*\d)(?=.*[a-záéíóúüñ])(?=.*[A-ZÁÉÍÓÚÜÑ])(?=.*[()\/&%$#.@])/;
                        if (!regex.test($("#" + control.id).val()) || $("#" + control.id).val().length < 8) {
                            AddTextErrorToInput(control, "La contraseña no cumple con los requerimientos mínimos.");
                            return false;
                        }
                        else if (Passwords[0].value === Passwords[1].value) {
                            control = Passwords[0];
                            div.removeClass("has-error");
                            div.addClass("has-success has-feedback");
                            $("#glypcn" + control.id).remove();
                            div.append('<span id="glypcn' + control.id + '" class="glyphicon glyphicon-ok form-control-feedback "></span>');

                            $("#glypcnNovalid2" + control.id).remove();
                            $("#glypcnNovalid3" + control.id).remove();
                            $("#glypcn" + control.id).remove();
                            div.append('<span id="glypcnNovalid2' + control.id + '" class="glyphicon glyphicon-ok form-control-feedback"></span>');

                            control = Passwords[1];

                            div.removeClass("has-error");
                            div.addClass("has-success has-feedback");
                            $("#glypcn" + control.id).remove();
                            div.append('<span id="glypcn' + control.id + '" class="glyphicon glyphicon-ok form-control-feedback "></span>');

                            $("#glypcnNovalid2" + control.id).remove();
                            $("#glypcnNovalid3" + control.id).remove();
                            $("#glypcn" + control.id).remove();
                            div.append('<span id="glypcnNovalid2' + control.id + '" class="glyphicon glyphicon-ok form-control-feedback"></span>');
                            return true;
                        }
                        else {
                            control = Passwords[0];

                            div.removeClass("has-error");
                            div.addClass("has-success has-feedback");
                            $("#glypcn" + control.id).remove();
                            div.append('<span id="glypcn' + control.id + '" class="glyphicon glyphicon-ok form-control-feedback "></span>');
                            div.addClass("has-error has-feedback");
                            $("#glypcnNovalid2" + control.id).remove();
                            $("#glypcnNovalid3" + control.id).remove();
                            $("#glypcn" + control.id).remove();
                            div.append('<span id="glypcnNovalid2' + control.id + '" class="glyphicon glyphicon-remove form-control-feedback  "></span>');

                            control = Passwords[1];

                            div.removeClass("has-error");
                            div.addClass("has-success has-feedback");
                            $("#glypcn" + control.id).remove();
                            div.append('<span id="glypcn' + control.id + '" class="glyphicon glyphicon-ok form-control-feedback "></span>');

                            div.addClass("has-error has-feedback");
                            $("#glypcnNovalid2" + control.id).remove();
                            $("#glypcnNovalid3" + control.id).remove();
                            $("#glypcn" + control.id).remove();
                            div.append('<span id="glypcnNovalid2' + control.id + '" class="glyphicon glyphicon-remove form-control-feedback  "></span>');
                            div.append('<span id="glypcnNovalid3' + control.id + '" class="help-block">Las contraseñas no coinciden</span>');
                            return false;
                        }
                    }
                }
                break;
            case "email":
                if ($("#" + control.id).val() == null || $("#" + control.id).val() == "") {
                    div.removeClass("has-success");
                    $("#glypcn" + control.id).remove();
                    div.addClass("has-error has-feedback");
                    div.append('<span id="glypcn' + control.id + '" class="glyphicon glyphicon-remove form-control-feedback"></span>');

                    return false;
                }
                else {
                    div.removeClass("has-error");
                    div.addClass("has-success has-feedback");
                    $("#glypcn" + control.id).remove();


                    // Expresion regular para validar el correo
                    var regex = /[\w-\.]{2,}@([\w-]{2,}\.)*([\w-]{2,}\.)[\w-]{2,4}/;

                    // Se utiliza la funcion test() nativa de JavaScript
                    if (!regex.test($('#' + control.id).val().trim())) {
                        div.addClass("has-error has-feedback");
                        div.append('<span id="glypcn' + control.id + '" class="glyphicon glyphicon-remove form-control-feedback "></span>')
                        $("#glypcnNovalid" + control.id).remove();
                        div.append('<span id="glypcnNovalid' + control.id + '" class="help-block">Correo Electronico no valido</span>')
                        return false;
                    }
                    else {
                        $("#glypcnNovalid" + control.id).remove();
                        div.append('<span id="glypcn' + control.id + '" class="glyphicon glyphicon-ok form-control-feedback"></span>');

                    }
                    return true;
                }
                break;
            case "date":
                //debugger;
                if ($("#" + control.id).val() == null || $("#" + control.id).val() == "") {
                    div.removeClass("has-success");
                    $("#glypcn" + control.id).remove();
                    div.addClass("has-error has-feedback");
                    div.append('<span id="glypcn' + control.id + '" class="glyphicon glyphicon-remove form-control-feedback"></span>');

                    return false;
                }
                else {
                    div.removeClass("has-error");
                    div.addClass("has-success has-feedback");
                    $("#glypcn" + control.id).remove();

                    //debugger;
                    // Expresion regular para validar el correo
                    // var regex = /^(?:(?:31(\/|-|\.)(?:0?[13578]|1[02]|(?:Jan|Mar|May|Jul|Aug|Oct|Dec)))\1|(?:(?:29|30)(\/|-|\.)(?:0?[1,3-9]|1[0-2]|(?:Jan|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec))\2))(?:(?:1[6-9]|[2-9]\d)?\d{2})$|^(?:29(\/|-|\.)(?:0?2|(?:Feb))\3(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00))))$|^(?:0?[1-9]|1\d|2[0-8])(\/|-|\.)(?:(?:0?[1-9]|(?:Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep))|(?:1[0-2]|(?:Oct|Nov|Dec)))\4(?:(?:1[6-9]|[2-9]\d)?\d{2})$/;
                    var regex = /^(?:3[01]|[12][0-9]|0?[1-9])([\-/.])(0?[1-9]|1[1-2])\1\d{4}$/;
                    // Se utiliza la funcion test() nativa de JavaScript
                    if (!regex.test($('#' + control.id).val().trim())) {
                        div.addClass("has-error has-feedback");
                        div.append('<span id="glypcn' + control.id + '" class="glyphicon glyphicon-remove form-control-feedback "></span>')
                        $("#glypcnNovalid" + control.id).remove();
                        div.append('<span id="glypcnNovalid' + control.id + '" class="help-block">Fecha invalida</span>')
                        return false;
                    }
                    else {
                        $("#glypcnNovalid" + control.id).remove();
                        div.append('<span id="glypcn' + control.id + '" class="glyphicon glyphicon-ok form-control-feedback"></span>');

                    }
                    return true;
                }
                break;
            case "checkbox":
                if (!$("#" + control.id).is(':checked')) {
                    var div = $("#" + control.id).closest("div");
                    div.removeClass("has-success");
                    $("#glypcn" + control.id).remove();
                    div.addClass("has-error has-feedback");
                    div.append('<span id="glypcn' + control.id + '" class="glyphicon glyphicon-remove form-control-feedback ajustar "></span>');
                    return false;
                }
                else {
                    var div = $("#" + control.id).closest("div");
                    div.removeClass("has-error");
                    div.addClass("has-success has-feedback");
                    $("#glypcn" + control.id).remove();
                    div.append('<span id="glypcn' + control.id + '" class="glyphicon glyphicon-ok form-control-feedback ajustar "></span>');
                    return true;
                }
                break;
            case "select-one":
                if ($("#" + control.id).val() == null || $("#" + control.id).val() == "" || $("#" + control.id).val() == "0") {
                    div.removeClass("has-success");
                    $("#glypcn" + control.id).remove();
                    div.addClass("has-error has-feedback");
                    div.append('<span id="glypcn' + control.id + '" class="glyphicon glyphicon-remove form-control-feedback "></span>');
                    return false;
                }
                else {
                    div.removeClass("has-error");
                    div.addClass("has-success has-feedback");
                    $("#glypcn" + control.id).remove();
                    div.append('<span id="glypcn' + control.id + '" class="glyphicon glyphicon-ok form-control-feedback "></span>');
                    return true;
                }
                break;
            default:

                break;
        }
    }
    return true;
}

function ValidarCtrlPass(control, validarPass) {
    if (control.id != "") {
        RemoveValidateText(control);
        if ($(control).parent()[0].nodeName == "TD")
            div = $(control).closest("TD");
        else
            div = $(control).closest("div");

        if ($("#" + control.id).val() == null || $("#" + control.id).val() == "") {
            div.removeClass("has-success");
            $("#glypcn" + control.id).remove();
            div.addClass("has-error has-feedback");
            div.append('<span id="glypcn' + control.id + '" class="glyphicon glyphicon-remove form-control-feedback "></span>');
            return false;
        }
        else {
            var regex = /^(?=.*\d)(?=.*[a-záéíóúüñ])(?=.*[A-ZÁÉÍÓÚÜÑ])(?=.*[()\/&%$#.@])/;
            var errorRegex = !regex.test($("#" + control.id).val()) || !($("#" + control.id).val().length >= 8) ? false : true;
            if (!errorRegex && validarPass) {
                //div.removeClass("has-success");
                //$("#glypcn" + control.id).remove();
                //div.addClass("has-error has-feedback");
                //div.append('<span id="glypcn' + control.id + '" class="glyphicon glyphicon-remove form-control-feedback "></span>');
                AddTextErrorToInput(control, "La contraseña no cumple con los requerimientos mínimos.");
                //AddTextErrorToInput(control, "La contraseña debe tener ");
                return false;
            }
            else {
                div.removeClass("has-error");
                div.addClass("has-success has-feedback");
                $("#glypcn" + control.id).remove();
                div.append('<span id="glypcn' + control.id + '" class="glyphicon glyphicon-ok form-control-feedback "></span>');
                return true;
            }
        }
    }
}

function ValidarPasswordIguales(control1, control2) {
    RemoveValidateText(control1);
    RemoveValidateText(control2);
    if ($(control1).parent()[0].nodeName == "TD")
        div = $(control1).closest("TD");
    else
        div = $(control1).closest("div");
    if (control1.value == control2.value) {
        div.removeClass("has-error");
        div.addClass("has-success has-feedback");
        $("#glypcn" + control1.id).remove();
        div.append('<span id="glypcn' + control1.id + '" class="glyphicon glyphicon-ok form-control-feedback "></span>');
        return true;
    } else {
        AddTextErrorToInput(control1, "Las contraseñas no coinciden");
        AddTextErrorToInput(control2, "");
        return false;
    }
}

function removerValidacion(arraycontroles, removetext) {
    $.each(arraycontroles, function (index, value) {
        RemoveValidateText(document.getElementById(value), removetext);
    });
}
function RemoveValidateText(control, removetext) {
    if (control.id != "") {
        if (removetext)
            $(control).val('');
        if ($(control).parent()[0].nodeName == "TD")
            div = $(control).closest("TD");
        else
            div = $(control).closest("div");
        div.removeClass("has-success");
        $("#glypcn" + control.id).remove();
        $("#glypcnNovalid" + control.id).remove();
        div.removeClass("has-error");
        $("#glypcn" + control.id).remove();
    }
}

function CargarFechaInicioFechaFin(fechaInicio, fechaFin, formato) {
    $('#' + fechaInicio).datepicker({
        minDate: 0,
        dateFormat: formato || 'dd/mm/yy',
        beforeShow: function () {
            setTimeout(function () {
                $('.ui-datepicker').css('z-index', 99999);
            }, 0);
        },
        onSelect: function (dateText, datePickerInstance) {
            debugger;
            var oneDay = 24 * 60 * 60 * 1000; // hours*minutes*seconds*milliseconds
            var firstDate = new Date();
            var secondDate = new Date(datePickerInstance.selectedYear, datePickerInstance.selectedMonth, datePickerInstance.selectedDay);

            var diffDays = Math.round(Math.abs((firstDate.getTime() - secondDate.getTime()) / (oneDay)));
            $('#' + fechaFin).val('');
            $('#' + fechaFin).datepicker("destroy");
            // $('#txtFechaHasta').removeClass("hasDatepicker").removeAttr('id');
            // $('#txtFechaHasta').unbind();
            if (firstDate.getHours() > 13) {
                diffDays += 1;
            }
            setTimeout(function () {
                $('#' + fechaFin).datepicker({
                    minDate: diffDays,
                    dateFormat: formato || 'dd/mm/yy'
                });
                $('#' + fechaFin).focus();
            }, 2);

        }
    })
  .on("change", function () {
      debugger;

      var regex = /^(?:(?:31(\/|-|\.)(?:0?[13578]|1[02]))\1|(?:(?:29|30)(\/|-|\.)(?:0?[1,3-9]|1[0-2])\2))(?:(?:1[6-9]|[2-9]\d)?\d{2})$|^(?:29(\/|-|\.)0?2\3(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00))))$|^(?:0?[1-9]|1\d|2[0-8])(\/|-|\.)(?:(?:0?[1-9])|(?:1[0-2]))\4(?:(?:1[6-9]|[2-9]\d)?\d{2})$/;
      var hoy = new Date().getTime();
      var parts = this.value.split('/');
      var mydate = new Date(parts[2], parts[1] - 1, parts[0], 23, 59, 59, 0).getTime();




      if (this.value != "")
          if (!regex.test($(this).val().trim()) || mydate < hoy) {
              this.value = "";
              //div = $(this).closest("div");
              //div.addClass("has-error has-feedback");
              //div.append('<span id="glypcn' + this.id + '" class="glyphicon glyphicon-remove form-control-feedback "></span>')
              //$("#glypcnNovalid" + this.id).remove();
              //div.append('<span id="glypcnNovalid' + this.id + '" class="help-block">Fecha invalida (dd/mm/yyyy)</span>')
          }
      //else {
      //    RemoveValidateText(this);
      //}
  });
    $('#' + fechaInicio).next().click(function () {
        $('#' + fechaInicio).focus();
    });
};
function CargarFecha(fecha, formato) {
    $('#' + fecha).datepicker({
        //minDate: 0,
        dateFormat: formato || 'dd/mm/yy',
        beforeShow: function () {
            setTimeout(function () {
                $('.ui-datepicker').css('z-index', 99999);
            }, 0);
        }
    })
    .on("change", function () {
        debugger;

        //var regex = /^(?:(?:31(\/|-|\.)(?:0?[13578]|1[02]))\1|(?:(?:29|30)(\/|-|\.)(?:0?[1,3-9]|1[0-2])\2))(?:(?:1[6-9]|[2-9]\d)?\d{2})$|^(?:29(\/|-|\.)0?2\3(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00))))$|^(?:0?[1-9]|1\d|2[0-8])(\/|-|\.)(?:(?:0?[1-9])|(?:1[0-2]))\4(?:(?:1[6-9]|[2-9]\d)?\d{2})$/;

        //if (this.value != "")
        //    if (!regex.test($(this).val().trim())) {
        //        this.value = "";
        //        //div = $(this).closest("div");
        //        //div.addClass("has-error has-feedback");
        //        //div.append('<span id="glypcn' + this.id + '" class="glyphicon glyphicon-remove form-control-feedback "></span>')
        //        //$("#glypcnNovalid" + this.id).remove();
        //        //div.append('<span id="glypcnNovalid' + this.id + '" class="help-block">Fecha invalida (dd/mm/yyyy)</span>')
        //    }
        ////else {
        ////    RemoveValidateText(this);
        ////}
    })
}


function ShowMessage(TituloMensaje, Mensaje, TipoMensaje, FuncionAceptar, FuncionCancelar, NombreAceptar, NombreCancelar) {
    switch (TipoMensaje) {
        case "AceptarCancelar":
            BootstrapDialog.show({
                message: Mensaje,
                closable: false,
                title: TituloMensaje,
                buttons: [{
                    icon: 'glyphicon glyphicon-send',
                    label: NombreAceptar || 'Aceptar',
                    cssClass: 'btn-primary',
                    autospin: true,
                    action: function (dialogRef) {
                        dialogRef.enableButtons(false);
                        dialogRef.setClosable(false);
                        dialogRef.getModalBody().html('Espere un momento por favor...');
                        if (FuncionAceptar) {
                            FuncionAceptar(dialogRef);
                        }
                        else {
                            setTimeout(function () {
                                dialogRef.close();
                            }, 3000);
                        }
                    }
                }, {
                    label: NombreCancelar || 'Cancelar',
                    action: function (dialogRef) {
                        if (FuncionCancelar) {
                            FuncionCancelar(dialogRef);
                        } else {
                            dialogRef.close();
                        }
                    }
                }]
            });
            break;
        case "Alerta":
            BootstrapDialog.show({
                message: Mensaje,
                closable: false,
                title: TituloMensaje,
                buttons: [{
                    label: 'Aceptar',
                    action: function (dialogRef) {
                        if (FuncionAceptar) {
                            FuncionAceptar(dialogRef);
                        } else {
                            dialogRef.close();
                        }
                    }
                }]
            });
            break;

        case "SoloMensaje":
            BootstrapDialog.show({
				//title: 'ASOCAJAS - ANI',
                message: Mensaje,
                closable: true,
                //title: TituloMensaje,
                buttons: [{
                    closable: true,
                    //cssClass: 'btn-primary',
					cssClass: 'btn-mensaje',
                    label: 'Aceptar',
                    action: function (dialogRef) {
                //        if (FuncionAceptar) {
                //            FuncionAceptar(dialogRef);
                //        } else {
                            dialogRef.close();
                //        }
                    }
                }]




            });
            break;

    }
}

function SessionState() {
    setTimeout(function () {
        consumirServicio(ServiceUrl + "RUsuario/GetIsLogin", null, function (data) {
            //PostService(location.origin + '/Services/Servicios.aspx/IsLogin', null, function (data) {
            if (!data.Ok) {
                window.location.href = location.origin + "/Pages/Login.aspx";
            } else
                SessionState();
        });
    }, 20000);
}

$(document).ready(function () {
    $('body').hide();
    debugger;
    setTimeout(function () {
        if (location.pathname != "/Pages/Login.aspx" && location.pathname != "/Pages/Modificacion_Contrasena.aspx") {// && location.pathname != "/Pages/AllPages/Gestion_Usuarios.aspx") {
            consumirServicio(ServiceUrl + "RUsuario/GetIsLogin", null, function (data) {
                //PostService(location.origin + '/Services/Servicios.aspx/IsLogin', null, function (data) {
                if (!data.Ok) {
                    window.location.href = location.origin + "/Pages/Login.aspx";
                } else {
                    $('body').show();
                    consumirServicio(ServiceUrl + "RUsuario/GetRUsuarioByMail", null, function (dataUsuario) {
                        $('#UsuarioLogueado').html(dataUsuario.Nombre + " " + dataUsuario.Apellido);
                    });
                }
            });
            SessionState();
        } else
            $('body').show();
    }, 500);
});

function SessionLogin(valueUser, functionsucess) {
    //PostService(location.origin + '/Services/Servicios.aspx/Login', "{UserData: '" + valueUser + "'}", functionsucess);
}

function Logout() {
    debugger;
    consumirServicio(ServiceUrl + "RUsuario/GetCloseSession", null);
        //PostService(location.origin + '/Services/Servicios.aspx/Logout', null);
}

function CerrarSesion() {
    window.location.href = location.origin + "/Pages/Login.aspx"
}

    //location.origin + '../Services/Servicios.aspx/VerifyCaptcha'
    //"{response: '" + response + "'}"
function PostService(uri, data, functionSucces) {
    $.ajax({
        type: "POST",
        url: uri,
        data: data,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (functionSucces) {
                functionSucces(jQuery.parseJSON(data.d));
            }
        }
    });
}

function BuscarTable(IdTxtFilter, IdTbody) {
    $('#' + IdTxtFilter).keyup(function () {
        // debugger; 
        var rex = new RegExp($(this).val(), 'i');
        $('#' + IdTbody + ' tr').hide();
        $('#' + IdTbody + ' tr').filter(function () {
            return rex.test($(this).text());
        }).show();

    })
}

function convertTextToDate(strDate) {
    var dateSplit = strDate.split("/");

    return (dateSplit[2] + "/" + dateSplit[1] + "/" + dateSplit[0]);

}

function ConvertDateSQLToText(strdate) {
    var dateSplit = strdate.split("T")[0].split("-");
    return (dateSplit[2] + "/" + dateSplit[1] + "/" + dateSplit[0])
}

// ServiceUrl + "Home/AjaxGetJsonData"
//{data:"Nombre",orderable:false,ctroFilter:"txtNombreFilter"}
var downloading = false,
    downloadTimestamp = null;
function SetDataTable(idTable, DireccionURL, dataColumns) {
    debugger;
    var columns = new Array();
    $.each(dataColumns, function (index, value) {
        columns.push({ data: value.data, "orderable": false });
    });

    $('#' + idTable).dataTable({
        "language": {
            "sProcessing": "Procesando...",
            "sLengthMenu": "Mostrar _MENU_ registros",
            "sZeroRecords": "No se encontraron resultados",
            "sEmptyTable": "Ningún dato disponible en esta tabla",
            "sInfo": "Mostrando registros del _START_ al _END_ de un total de _TOTAL_ registros",
            "sInfoEmpty": "Mostrando registros del 0 al 0 de un total de 0 registros",
            "sInfoFiltered": "(filtrado de un total de _MAX_ registros)",
            "sInfoPostFix": "",
            "sSearch": "Buscar:",
            "sUrl": "",
            "sInfoThousands": ",",
            "sLoadingRecords": "Cargando...",
            "oPaginate": {
                "sFirst": "Primero",
                "sLast": "Último",
                "sNext": "Siguiente",
                "sPrevious": "Anterior"
            },
            "oAria": {
                "sSortAscending": ": Activar para ordenar la columna de manera ascendente",
                "sSortDescending": ": Activar para ordenar la columna de manera descendente"
            }
        },
        dom: 'Bfrtip',
        buttons: [{
            text: 'CSV',
            titleAttr: 'CSV',
            className: 'downloadCSV',
            action: function (e, dt, node, config) {
                if (downloading === false) { //if download is in progress, do nothing, else
                    node.attr('disabled', 'disabled'); //disable download button to prevent multi-click, probably some sort of *busy* indicator is a good idea

                    downloading = true; //set downloading status to *true*

                    dt.ajax.reload(); //re-run *DataTables* AJAX query with current filter and sort applied
                }
            }
        }],
        "searching": true,
        "processing": true, // control the processing indicator.
        "serverSide": true, // recommended to use serverSide when data is more than 10000 rows for performance reasons
        "info": true,   // control table information display field
        "stateSave": true,  //restore table state on page reload,
        "lengthMenu": [[10, 20, 50], [10, 20, 50]],    // use the first inner array as the page length values and the second inner array as the displayed options
        "ajax": {
            "url": DireccionURL,
            "type": "POST",
            "contentType": "application/json; charset=utf-8",
            "data": function (d) {
                d.timestamp = new Date().getTime(); //add timestamp to data to be sent, it's going to be useful when retrieving produced file server-side

                downloadTimestamp = d.timestamp; //save timestamp in local variable for use with GET request when retrieving produced file client-side

                if (downloading === true) { //if download button was clicked
                    d.download = true; //tell server to prepare data for download
                    downloading = d.draw; //set which *DataTable* draw is actually a request to produce file for download
                }

                return JSON.stringify({ parameters: d });
            }
        },
        "columns": columns,
        "order": [[0, "asc"]],
        "preDrawCallback": function (settings) {
            if (settings.iDraw === downloading) { //if returned *DataTable* draw matches file request draw value
                downloading = false; //set downloading flag to false

                $('.downloadCSV').removeAttr('disabled'); //enable download button
                var filename = "my_data.csv";
                var blob = new Blob([settings.json.DownloadStr], { type: 'text/csv;charset=utf-8;' });
                if (navigator.msSaveBlob) { // IE 10+
                    navigator.msSaveBlob(blob, filename);
                } else {
                    var link = document.createElement("a");
                    if (link.download !== undefined) { // feature detection
                        // Browsers that support HTML5 download attribute
                        var url = URL.createObjectURL(blob);
                        link.setAttribute("href", url);
                        link.setAttribute("download", filename);
                        link.style.visibility = 'hidden';
                        document.body.appendChild(link);
                        link.click();
                        document.body.removeChild(link);
                    }
                }
                //var encodedUri = encodeURI(settings.json.DownloadStr);
                //var link = document.createElement("a");
                //link.setAttribute("href", encodedUri);
                //link.setAttribute("download", "my_data.csv");
                //document.body.appendChild(link); // Required for FF

                //link.click();

                //window.location.href = URL + '?' + $.param({ ts: downloadTimestamp }); //navigate to AJAX URL with timestamp as parameter to trigger file download. Or You can have hidden IFrame and set its *src* attribute to the address above.

                return false; //as it is file request, table should not be re-drawn
            }
        }
    })

}


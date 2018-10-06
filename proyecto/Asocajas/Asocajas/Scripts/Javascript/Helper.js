function CargarFechaInicioFechaFin(fechaInicio, fechaFin, formato) {
    $('#' + fechaInicio).datepicker({
        minDate: 0,
        dateFormat: formato || 'dd/mm/yy',
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
}
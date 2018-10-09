<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/AllPages/pages.Master" AutoEventWireup="true" CodeBehind="LogsEventos.aspx.cs" Inherits="Asocajas.Pages.LogsEventos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Scripts/css/Login/tables/bootstrap-table.min.css" rel="stylesheet" />
    <link href="../Scripts/css/Login/tables/bootstrap-editable.css" rel="stylesheet" />

    <script src="../Scripts/Scripts/tables/bootstrap-table.js"></script>
    <script src="../Scripts/Scripts/tables/bootstrap-table-editable.js"></script>
    <script src="../Scripts/Scripts/tables/bootstrap-table-export.js"></script>
    <script src="../Scripts/Scripts/tables/tableExport.js"></script>
    <script src="../Scripts/Scripts/tables/bootstrap-table-filter-control.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TitlePage" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="bodyContent" runat="server">

    <form>
        <div class="table table-responsive Autoheight">
        <div id="toolbar">
		
</div>
            <%--EJEMPLO1--%>
<table id="table" 
			 data-toggle="table"
			 data-search="true"
			 data-filter-control="true" 
			 data-show-export="true"
			 data-click-to-select="true"
			 data-toolbar="#toolbar"
       class="table-responsive">
	<thead>
		<tr>			
			<th data-field="ccf" data-filter-control="select" data-sortable="true">CCF</th>
			<th data-field="user" data-filter-control="select" data-sortable="true">Usuario</th>
            <th data-field="fechaIni" data-filter-control="input" data-sortable="true">Fecha Inicio</th>
            <th data-field="FechaFin" data-filter-control="input" data-sortable="true">Fecha Fin</th>			
		
		</tr>
	</thead>
	<tbody>
		
        <tr>			
			<td>Jitender</td>
			<td>01/09/2015</td>
			<td>Français</td>
			<td>12/20</td>
		</tr>
		<tr>
			
			<td>Jahid</td>
			<td>05/09/2015</td>
			<td>Philosophie</td>
			<td>8/20</td>
		</tr>
		<tr>
			
			<td>Valentin</td>
			<td>05/09/2015</td>
			<td>Philosophie</td>
			<td>4/20</td>
		</tr>
		<tr>
			
			<td>Milton</td>
			<td>05/09/2015</td>
			<td>Philosophie</td>
			<td>10/20</td>
		</tr>
		<tr>
			
			<td>Gonesh</td>
			<td>01/09/2015</td>
			<td>Français</td>
			<td>14/20</td>
		</tr>
		<tr>
			
			<td>Valérie</td>
			<td>07/09/2015</td>
			<td>Mathématiques</td>
			<td>19/20</td>
		</tr>
		<tr>
			
			<td>Valentin</td>
			<td>01/09/2015</td>
			<td>Français</td>
			<td>11/20</td>
		</tr>
		<tr>
			
			<td>Eric</td>
			<td>01/10/2015</td>
			<td>Philosophie</td>
			<td>8/20</td>
		</tr>
		<tr>
			
			<td>Valentin</td>
			<td>07/09/2015</td>
			<td>Mathématiques</td>
			<td>14/20</td>
		</tr>
		<tr>
			
			<td>Valérie</td>
			<td>01/10/2015</td>
			<td>Philosophie</td>
			<td>12/20</td>
		</tr>
		<tr>
			
			<td>Eric</td>
			<td>07/09/2015</td>
			<td>Mathématiques</td>
			<td>14/20</td>
		</tr>
		<tr>
		
			<td>Valentin</td>
			<td>01/10/2015</td>
			<td>Philosophie</td>
			<td>10/20</td>
		</tr>
	</tbody>
</table>
 </div>
          <%-- EJEMPLO2--%>
<h2>EJEMPLO2 </h2>
 <div class="table-responsive Autoheight">
<table class="table table-bordered">
<thead>
<tr>
<td> 
    <select class="form-control" name="cboEstado" id="">
    <option value="">CFC</option>
</select>

</td>
<td>   
     <select class="form-control" name="cboEstado" id="">
    <option value="">Usuario</option>
</select>

</td>

<td>
     <div class="form-group">
                <div class='input-group date' id='datetimepicker1'>
                    <input type='text' class="form-control" />
                    <span class="input-group-addon">
                        <span class="glyphicon glyphicon-calendar"></span>
                    </span>
                </div>
            </div>
</td>
<td>
   
    <div class="form-group">
                <div class='input-group date' id='Div1'>
                    <input type='text' class="form-control" />
                    <span class="input-group-addon">
                        <span class="glyphicon glyphicon-calendar"></span>
                    </span>
                </div>
            </div>
</td>
<th colspan="4"> 
    <button type="button" class="btn  btn-primary">Consultar</button></th>

</tr>
    <tr>
<th>CCF</th>
<th>Usuario</th>
<th>Inicio sesión</th>
<th>Fin sesión</th>
<th>Tipo</th>
<th>Consualta Reporte Transacción</th>
<th>Consulta Reporte excepcciones</th>
<th>Consulta Reporte eventos</th>
</tr>
</thead>
<tbody>
<tr>
<td>Cellxxxxxxxxxxx</td>
<td>Cellxxxxxxxxxxx</td>
<td>Cellxxxxxxxxxxx</td>
<td>Cellxxxxxxxxxxx</td>
<td>Cellxxxxxxxxxxx</td>
<td>Cellxxxxxxxxxxx</td>
<td>Cellxxxxxxxxxxx</td>
<td>Cellxxxxxxxxxxx</td>
</tr>
<tr>
<td>Cell</td>
<td>Cell</td>
<td>Cell</td>
<td>Cell</td>
<td>Cell</td>
<td>Cell</td>
<td>Cell</td>
<td>Cell</td>
</tr>
<tr>
<td>Cell</td>
<td>Cell</td>
<td>Cell</td>
<td>Cell</td>
<td>Cell</td>
<td>Cell</td>
<td>Cell</td>
<td>Cell</td>
</tr>
</tbody>
</table>
      
</div>

      
    </form>
    <script>

        //exporte les données sélectionnées
        var $table = $('#table');
        $(function () {
            $('#toolbar').find('select').change(function () {
                $table.bootstrapTable('refreshOptions', {
                    exportDataType: $(this).val()
                });
            });
        })

        var trBoldBlue = $("table");

        $(trBoldBlue).on("click", "tr", function () {
            $(this).toggleClass("bold-blue");
        });
    </script>
</asp:Content>

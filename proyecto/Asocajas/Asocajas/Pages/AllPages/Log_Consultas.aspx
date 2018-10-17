<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/AllPages/pages.Master" AutoEventWireup="true" CodeBehind="Log_Consultas.aspx.cs" Inherits="Asocajas.Pages.AllPages.Log_Consultas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../Scripts/css/Login/tables/bootstrap-table.min.css" rel="stylesheet" />
    <link href="../../Scripts/css/Login/tables/bootstrap-editable.css" rel="stylesheet" />

    <script src="../../Scripts/Scripts/tables/bootstrap-table.js"></script>
    <script src="../../Scripts/Scripts/tables/bootstrap-table-editable.js"></script>
    <script src="../../Scripts/Scripts/tables/bootstrap-table-export.js"></script>
    <script src="../../Scripts/Scripts/tables/tableExport.js"></script>
    <script src="../../Scripts/Scripts/tables/bootstrap-table-filter-control.js"></script>
    <script src="../../Scripts/Javascript/log_Consultas.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TitlePage" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="bodyContent" runat="server">
      <ul class="breadcrumb">
        <li><a href="Inicio.aspx">Administración</a></li>
        <li class="active">Consulta Transacciones</li>
    </ul>
    <h4><b>Consulta Transacciones</b></h4>
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
            <th data-field="transaccion" data-filter-control="input" data-sortable="true">Id . Transacción</th>
            <th data-field="via" data-filter-control="input" data-sortable="true">Vía</th>
            <th data-field="estado" data-filter-control="input" data-sortable="true">Estado</th>	
            <th data-field="consecutivo" data-filter-control="input" data-sortable="true">Consecutivo</th>
            <th data-field="mac" data-filter-control="input" data-sortable="true">Mac Address</th>	
            <th data-field="fechas" data-filter-control="input" data-sortable="true">Rango de fechas</th>					
		
		</tr>
	</thead>
    <tbody id="tbody">

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

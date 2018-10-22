<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/AllPages/pages.Master" AutoEventWireup="true" CodeBehind="Log_Consultas_ANI.aspx.cs" Inherits="Asocajas.Pages.AllPages.Log_Consultas_ANI" %>
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
        <li><a href="Inicio.aspx">Reportes Uso </a></li>
        <li class="active">Consultas ANI</li>
    </ul>
    <h4 class="text-center "><b>Reporte Consultas ANI</b></h4>
     <form>
	 
	
        <div class="encabezadoTable">
		<!--
            <div class="col-xs-2 col-sm-2 col-md-2">
                <div class="textEncabezado">
                    <span>Eventos Plataforma</span>
                </div>
            </div>
		-->	
            <div class="col-xs-12 col-sm-12 col-md-12">					
                    <div class="col-xs-12 col-sm-12 col-md-10 ">
						<div class="col-xs-6 col-sm-6 col-md-3 ">
							<div class="col-xs-12 col-sm-12 col-md-12 txtEncabezadoLog">
								<span>USUARIO</span>
							</div>
							<div class="col-xs-12 col-sm-12 col-md-12 ">
								<select class="form-control">
									<option value="1"></option>
								</select>
							</div>
						</div>
						<div class="col-xs-6 col-sm-6 col-md-3 ">
							<div class="col-xs-12 col-sm-12 col-md-12 txtEncabezadoLog">
								<span>FECHA INICIAL</span>
							</div>
							<div class="col-xs-12 col-sm-12 col-md-12 ">
                                <input type='date' class="form-control" name="LogInicia" id="LogInicia" placeholder="YYYY-MM-DD" required/>
							</div>
						</div>
						<div class="col-xs-6 col-sm-6 col-md-3 ">
							<div class="col-xs-12 col-sm-12 col-md-12 txtEncabezadoLog">
								<span>FECHA FINAL</span>
							</div>
							<div class="col-xs-12 col-sm-12 col-md-12 ">
                                <input type='date' class="form-control" name="LogFinaliza" id="LogFinaliza" placeholder="YYYY-MM-DD" required/>
							</div>
						</div>
						
                    </div>

                    <div class="col-xs-4 offset-xs-4 col-sm-4 offset-sm-4 col-md-2 offset-md-0 nuevoUser">	
						<span></span>
                        <button type="button" class="btn btn-success" >Consultar</button>
                    </div>
                
            </div>
        </div>
	
	 
	 
        <div class="table table-responsive Autoheight">
        <div id="toolbar">
		
</div>


              <table id="tblLogConsultasAni" class="display" style="width:100%">
        <thead>
            <tr>
                <th>CCF</th>
                <th>Usuario</th>
                <th>Id . Transacción</th>
                <th>Vía</th>
                <th>Vía</th>
                <th>Estado</th>
                <th>Consecutivo</th>
                <th>Mac Address</th>
                <th>Rango de fechas</th>
            </tr>
        </thead>
    </table>
            </div>
            <%--EJEMPLO1--%>
<%--<table id="table" 
			 data-toggle="table"
			 data-search="true"
			 data-filter-control="true" 
			 data-show-export="true"
			 data-click-to-select="true"
			 data-toolbar="#toolbar"
       class="table-responsive">
	<thead>
		<tr>			
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
</table>--%>
 </div>



    </form>
  <%--  <script>

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
    </script>--%>
</asp:Content>

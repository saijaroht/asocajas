<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/AllPages/pages.Master" AutoEventWireup="true" CodeBehind="LogsEventos.aspx.cs" Inherits="Asocajas.Pages.LogsEventos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../Scripts/css/Login/tables/bootstrap-table.min.css" rel="stylesheet" />
    <link href="../../Scripts/css/Login/tables/bootstrap-editable.css" rel="stylesheet" />

    <script src="../../Scripts/Scripts/tables/bootstrap-table.js"></script>
    <script src="../../Scripts/Scripts/tables/bootstrap-table-editable.js"></script>
    <script src="../../Scripts/Scripts/tables/bootstrap-table-export.js"></script>
    <script src="../../Scripts/Scripts/tables/tableExport.js"></script>
    <script src="../../Scripts/Scripts/tables/bootstrap-table-filter-control.js"></script>
    <script src="../../Scripts/Javascript/LogsEventos.js" type="text/javascript"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TitlePage" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="bodyContent" runat="server">

    
     <ul class="breadcrumb">
        <li><a href="Inicio.aspx">Administración</a></li>
        <li class="active">Logs de Eventos</li>
    </ul>
    <h4 class="text-center "><b>Eventos Plataforma</b></h4>
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
								<span>CCF</span>
							</div>
							<div class="col-xs-12 col-sm-12 col-md-12 ">
								<select class="form-control">
									<option value="0">-- TODAS --</option>
									<option value="1">ASOCAJAS</option>
									<option value="2">CAMACOL</option>
									<option value="3">COMFENALCO ANTIOQUIA</option>
									<option value="4">COMFAMA</option>
									<option value="5">CAJACOPI</option>
									<option value="6">COMBARRANQUILLA</option>
									<option value="7">COMFAMILIAR ATLANTICO</option>
									<option value="8">COMFENALCO CARTAGENA</option>
									<option value="9">COMFAMILIAR CARTAGENA</option>
									<option value="10">COMFABOY</option>
									<option value="11">CONFA</option>
									<option value="13">COMFACA</option>
									<option value="14">COMFACAUCA</option>
									<option value="15">COMFACESAR</option>
									<option value="16">COMFACOR</option>
									<option value="21">CAFAM</option>
									<option value="22">COLSUBSIDIO</option>
									<option value="24">COMPENSAR</option>
									<option value="26">COMFACUNDI</option>
									<option value="29">COMFACHOCO</option>
									<option value="30">COMFAGUAJIRA</option>
									<option value="32">COMFAMILIAR HUILA</option>
									<option value="33">CAJAMAG</option>
									<option value="34">COFREM</option>
									<option value="35">COMFAMILIAR NARIÑO</option>
									<option value="36">COMFAORIENTE</option>
									<option value="37">COMFANORTE</option>
									<option value="38">CAFABA</option>
									<option value="39">CAJASAN</option>
									<option value="40">COMFENALCO SANTANDER</option>
									<option value="41">COMFASUCRE</option>
									<option value="43">COMFENALCO QUINDIO</option>
									<option value="44">COMFAMILIAR RISARALDA</option>
									<option value="46">CAFASUR</option>
									<option value="48">COMFATOLIMA</option>
									<option value="50">COMFENALCO TOLIMA</option>
									<option value="56">COMFENALCO VALLE</option>
									<option value="57">COMFANDI</option>
									<option value="63">COMFAMILIAR PUTUMAYO</option>
									<option value="64">CAJASAI</option>
									<option value="65">CAFAMAZ</option>
									<option value="67">COMFIAR</option>
									<option value="68">COMCAJA</option>
									<option value="69">COMFACASANARE</option>
								</select>
							</div>
						</div>
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
			<th data-field="ccf" data-filter-control="select" data-sortable="true">CCF</th>
			<th data-field="user" data-filter-control="select" data-sortable="true">Usuario</th>
            <th data-field="fechaIni" data-filter-control="input" data-sortable="true">Fecha Inicio</th>
            <th data-field="FechaFin" data-filter-control="input" data-sortable="true">Fecha Fin</th>			
		
		</tr>
		</thead>
		<tbody id="tbody">
      
		</tbody>
	</table>--%>
     <table id="tblLogEventos" class="display" style="width:100%">
        <thead>
            <tr>
			<th>CCF</th>
			<th>Usuario</th>
			<th>Evento</th>
			<th>FechaEvento</th>
            </tr>
        </thead>
    </table>
 </div>
          <%-- EJEMPLO2--%>
<%--<h2>EJEMPLO2 </h2>
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

      --%>
    </form>
    <script>

        //exporte les données sélectionnées
        //var $table = $('#table');
        //$(function () {
        //    $('#toolbar').find('select').change(function () {
        //        $table.bootstrapTable('refreshOptions', {
        //            exportDataType: $(this).val()
        //        });
        //    });
        //})

        //var trBoldBlue = $("table");

        //$(trBoldBlue).on("click", "tr", function () {
        //    $(this).toggleClass("bold-blue");
        //});
    </script>
</asp:Content>

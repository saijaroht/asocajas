<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/AllPages/pages.Master" AutoEventWireup="true" CodeBehind="Consulta_Documentos_Masiva.aspx.cs" Inherits="Asocajas.Pages.AllPages.Consulta_Documentos_Masiva" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../Scripts/css/Login/tables/bootstrap-table.min.css" rel="stylesheet" />
    <link href="../../Scripts/css/Login/tables/bootstrap-editable.css" rel="stylesheet" />

    <script src="../../Scripts/Scripts/tables/bootstrap-table.js"></script>
    <script src="../../Scripts/Scripts/tables/bootstrap-table-editable.js"></script>
    <script src="../../Scripts/Scripts/tables/bootstrap-table-export.js"></script>
    <script src="../../Scripts/Scripts/tables/tableExport.js"></script>
    <script src="../../Scripts/Scripts/tables/bootstrap-table-filter-control.js"></script>
    <script src="../../Scripts/Javascript/Consulta_Documentos_Masiva.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TitlePage" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="bodyContent" runat="server">
    <%--<ul class="breadcrumb">
        <li><a href="Inicio.aspx">Administración</a></li>
        <li class="active">Consulta Masiva Documentos</li>
    </ul>--%>
	
	<h4 class="text-center "><b>Consulta Masiva Documentos</b></h4>
	
	<div class="encabezadoTable">
        <div class="col-xs-12 col-sm-12 col-md-12">	
			<div class="col-md-3"></div>
            <div class="col-xs-8 col-sm-8 col-md-4 ">
				<div class="col-xs-12 col-sm-12 col-md-12 txtEncabezadoLog">
				  <span>Listado Documentos</span>
				</div>
				<div class="col-xs-12 col-sm-12 col-md-12" >
					<input type="file" class="filestyle" data-buttonBefore="true" data-buttonText="Cargar Archivo" id="inputFile">
				</div>
			</div>
			<div class="col-md-1"></div>
            <div class="col-xs-4 col-sm-4 col-md-2 nuevoUser">					
                <button type="button" class="btn btn-success" id="btnConsultar" onclick="BuscarData();" >Consultar</button>
            </div>
			
        </div>
			
	</div>
	
	<div class="row col-md-6 col-md-offset-3">
	  <h3 class="text-justify "><b>Para la consulta masiva debe tener en cuenta que:  </b></h3>
	  
	  <ul>
		<li type="circle"><h4 class="text-justify "><b>El archivos debe ser de texto sin ninguna codificación. </b></h4></li>
		<li type="circle"><h4 class="text-justify "><b>Por cada línea deb ir un número de documento. </b></h4></li>
		<li type="circle"><h4 class="text-justify "><b>Para los número de documento sólo se permiten caracteres numéricos. </b></h4></li>
		<li type="circle"><h4 class="text-justify "><b>Sólo se permite un máximo de cien (100) registros o líneas para ser procesadas. </b></h4></li>	  
	</div>
	
	<!--
    <div class="row">
        <div class="col-sm-3">
            <h4><b>Consulta Masiva Documentos</b></h4>
        </div>
          <div class="col-sm-3">
          <input type="file" class="filestyle" data-buttonBefore="true" data-buttonText="Cargar Archivo">
        </div>
        <div class="col-sm-3">
          <div class="form-group">
            <div class='input-group date' id='datetimepicker1'>
                
                <span class="input-group-addon" style="background: white;">
                    <span class="glyphicon glyphicon-search"></span>
                </span>
                <input class="form-control" id="myInput" type="text" placeholder="Buscar...">
            </div>
          </div>
        </div>
        <div class="col-sm-3">
          <div class="row">
            <div class="col-sm-6">
              <button type="button" class="btn btn-primary">Cargar</button>
			</div>
			<div class="col-sm-6">
              <button type="button" class="btn btn-primary">Consultar</button>
			</div>
          </div>
        </div>
        
    </div>
    -->
    
	<!--
         
    <table id="table" 
			 data-toggle="table"
			 <%--data-search="true"--%>
			 data-filter-control="true" 
			 data-show-export="true"
			 data-click-to-select="true"
			 data-toolbar="#toolbar"
       class="table-responsive">
	<thead>
		<tr>			
			<th class="text-center"data-sortable="true">Identificación</th>
			<th class="text-center"data-sortable="true">Primer Apellido</th>
            <th class="text-center"data-sortable="true">Particula</th>
            <th class="text-center"data-sortable="true">Segundo Apellido</th>
            <th class="text-center"data-sortable="true">Primer Nombre</th>
            <th class="text-center"data-sortable="true">Segundo Nombre</th>
            <th class="text-center"data-sortable="true">Fecha de Nacimiento</th>
            <th class="text-center"data-sortable="true">Municipio de Expedición</th>
            <th class="text-center"data-sortable="true">Departamento de Expedición</th>
            <th class="text-center"data-sortable="true">Fecha de Expedición</th>
            <th class="text-center"data-sortable="true">Estado del Documento</th>
            <th class="text-center"data-sortable="true">Numero de Resolución</th>
            <th class="text-center"data-sortable="true">Año Resolucíon</th>
            
           		
		
		</tr>
	</thead>
	
    <tbody id="tbodyConsultaMasiva">
    


	</tbody>
	</table>
 -->



   
    
   <%-- <div class="row">
        <div class="col-sm-11"></div>
    <div class="col-sm-1">
        <button type="button" class="btn btn-primary">Salir</button>
    </div>
    </div>--%>
        
        
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

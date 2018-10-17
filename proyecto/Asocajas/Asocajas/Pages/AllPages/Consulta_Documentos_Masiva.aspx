﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/AllPages/pages.Master" AutoEventWireup="true" CodeBehind="Consulta_Documentos_Masiva.aspx.cs" Inherits="Asocajas.Pages.AllPages.Consulta_Documentos_Masiva" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../Scripts/css/Login/tables/bootstrap-table.min.css" rel="stylesheet" />
    <link href="../../Scripts/css/Login/tables/bootstrap-editable.css" rel="stylesheet" />

    <script src="../../Scripts/Scripts/tables/bootstrap-table.js"></script>
    <script src="../../Scripts/Scripts/tables/bootstrap-table-editable.js"></script>
    <script src="../../Scripts/Scripts/tables/bootstrap-table-export.js"></script>
    <script src="../../Scripts/Scripts/tables/tableExport.js"></script>
    <script src="../../Scripts/Scripts/tables/bootstrap-table-filter-control.js"></script>
    <script src="../../Scripts/Javascript/LogsExcepciones.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TitlePage" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="bodyContent" runat="server">
    
    <div class="row">
        <div class="col-sm-3">
            <h4><b>Consulta Masiva Documentos</b></h4>
        </div>
        <div class="col-sm-5">
               <div class="form-group">

        
            <div class='input-group date' id='datetimepicker1'>
                <span class="input-group-addon ">
                    <a href="#"><span>Cargar Archivo</span></a>
                </span>
                <span class="input-group-addon" style="background: white;">
                    <span class="glyphicon glyphicon-search"></span>
                </span>
                <input class="form-control" id="myInput" type="text" placeholder="Search..">
            </div>
        
    </div>
        </div>
        <div class="col-sm-2">
             <button type="button" class="btn btn-primary">Cargar</button>
        </div>
        <div class="col-sm-2">
             <button type="button" class="btn btn-primary">Consultar</button>
        </div>
    </div>
    
    

         
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
    <tbody id="tbody">
        <tr>			
			<td>524689754</td>
			<td>Suaréz</td>
            <td>Sin particula</td>
			<td>Gamboa</td>
            <td>Adriana</td>
            <td>Marcela</td>
            <td>10/06/1980</td>
            <td>Bogotá D.C.</td>
            <td>Distrito Capital</td>
            <td>06/08/1998</td>
            <td>0</td>
            <td>2355</td>
            <td>2007</td>
		</tr>

         <tr>			
			<td>524689754</td>
			<td>Suaréz</td>
            <td>Sin particula</td>
			<td>Gamboa</td>
            <td>Adriana</td>
            <td>Marcela</td>
            <td>10/06/1980</td>
            <td>Bogotá D.C.</td>
            <td>Distrito Capital</td>
            <td>06/08/1998</td>
            <td>0</td>
            <td>2355</td>
            <td>2007</td>
		</tr>

       <tr>			
			<td>524689754</td>
			<td>Suaréz</td>
            <td>Sin particula</td>
			<td>Gamboa</td>
            <td>Adriana</td>
            <td>Marcela</td>
            <td>10/06/1980</td>
            <td>Bogotá D.C.</td>
            <td>Distrito Capital</td>
            <td>06/08/1998</td>
            <td>0</td>
            <td>2355</td>
            <td>2007</td>
		</tr>


        <tr>			
			<td>524689754</td>
			<td>Suaréz</td>
            <td>Sin particula</td>
			<td>Gamboa</td>
            <td>Adriana</td>
            <td>Marcela</td>
            <td>10/06/1980</td>
            <td>Bogotá D.C.</td>
            <td>Distrito Capital</td>
            <td>06/08/1998</td>
            <td>0</td>
            <td>2355</td>
            <td>2007</td>
		</tr>

       <tr>			
			<td>524689754</td>
			<td>Suaréz</td>
            <td>Sin particula</td>
			<td>Gamboa</td>
            <td>Adriana</td>
            <td>Marcela</td>
            <td>10/06/1980</td>
            <td>Bogotá D.C.</td>
            <td>Distrito Capital</td>
            <td>06/08/1998</td>
            <td>0</td>
            <td>2355</td>
            <td>2007</td>
		</tr>

       <tr>			
			<td>524689754</td>
			<td>Suaréz</td>
            <td>Sin particula</td>
			<td>Gamboa</td>
            <td>Adriana</td>
            <td>Marcela</td>
            <td>10/06/1980</td>
            <td>Bogotá D.C.</td>
            <td>Distrito Capital</td>
            <td>06/08/1998</td>
            <td>0</td>
            <td>2355</td>
            <td>2007</td>
		</tr>

       <tr>			
			<td>524689754</td>
			<td>Suaréz</td>
            <td>Sin particula</td>
			<td>Gamboa</td>
            <td>Adriana</td>
            <td>Marcela</td>
            <td>10/06/1980</td>
            <td>Bogotá D.C.</td>
            <td>Distrito Capital</td>
            <td>06/08/1998</td>
            <td>0</td>
            <td>2355</td>
            <td>2007</td>
		</tr>

       <tr>			
			<td>524689754</td>
			<td>Suaréz</td>
            <td>Sin particula</td>
			<td>Gamboa</td>
            <td>Adriana</td>
            <td>Marcela</td>
            <td>10/06/1980</td>
            <td>Bogotá D.C.</td>
            <td>Distrito Capital</td>
            <td>06/08/1998</td>
            <td>0</td>
            <td>2355</td>
            <td>2007</td>
		</tr>


	</tbody>
</table>
 </div>



    </form>
    
    <div class="row">
        <div class="col-sm-11"></div>
    <div class="col-sm-1">
        <button type="button" class="btn btn-primary">Salir</button>
    </div>
    </div>
        
        
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

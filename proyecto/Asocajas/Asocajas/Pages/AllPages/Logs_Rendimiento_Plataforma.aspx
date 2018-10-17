<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/AllPages/pages.Master" AutoEventWireup="true" CodeBehind="Logs_Rendimiento_Plataforma.aspx.cs" Inherits="Asocajas.Pages.AllPages.Logs_Rendimiento_Plataforma" %>
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
    <ul class="breadcrumb">
        <li><a href="Inicio.aspx">Administración</a></li>
        <li class="active">Logs Desempeño Plataforma</li>
    </ul>
   <h4><b>Desempeño Plataforma</b></h4>


    <div class="row">
        <div class="col-sm-3">
            <div class="radio">
                <label>
                    <input type="radio" name="optradio" checked>Rendimiento</label>
            </div>
            <div class="radio">
                <label>
                    <input type="radio" name="optradio">Carga y Estrés
                </label>
            </div>

            
        </div>
        <div class="col-sm-6">
                 <div class="form-group">
                                
                                <div class="col-sm-8">
                                    <div class='input-group date' id='datetimepicker1'>
                                        <span class="input-group-addon">
                                            <span >F inicial</span>
                                        </span>
                                        <input type='text' class="form-control styleinput" datepicker-popup="yyyy/mm/dd" name="txtFechaInicial" id="txtFechaInicial" placeholder="DD/MM/YYYY" datepicker required />
                                        <span class="input-group-addon">
                                            <span class="glyphicon glyphicon-calendar"></span>
                                        </span>
                                    </div>
                                </div>
                            </div>
                 <div class="form-group">
                                
                                <div class="col-sm-8">
                                    <div class='input-group date' id='Div1'>
                                        <span class="input-group-addon" >
                                            <span >F Final</span>
                                        </span>
                                        <input type='text' class="form-control styleinput" datepicker-popup="yyyy/mm/dd" name="txtFechaFinal" id="txtFechaFinal" placeholder="DD/MM/YYYY" datepicker required />
                                        <span class="input-group-addon">
                                            <span class="glyphicon glyphicon-calendar"></span>
                                        </span>
                                    </div>
                                </div>
                            </div>
            </div>
        <div class="col-sm-2">
            
            <button type="button" class="btn btn-primary">Consultar</button>
            
        </div>
    </div>

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
			<th class="text-center"data-field="ccf"  data-sortable="true">Tiempo de Respuesta</th>
			<th class="text-center"data-field="user"  data-sortable="true">Tiempo Promedio</th>
            <th class="text-center"data-field="fechaIni"  data-sortable="true">Hora Inicial</th>
            <th class="text-center"data-field="fechaFin" data-sortable="true">Hora Final</th>

		
		</tr>
	</thead>
    <tbody id="tbody">
        <tr>			
			<td>27 ms</td>
			<td>50.66 ms</td>
			<td>8:00:00 a.m.</td>
            <td>9:00:00 a.m.</td>
		</tr>

          <tr>			
			<td>27 ms</td>
			<td>50.66 ms</td>
			<td>8:00:00 a.m.</td>
            <td>9:00:00 a.m.</td>
		</tr>

         <tr>			
			<td>27 ms</td>
			<td>50.66 ms</td>
			<td>8:00:00 a.m.</td>
            <td>9:00:00 a.m.</td>
		</tr>

           <tr>			
			<td>27 ms</td>
			<td>50.66 ms</td>
			<td>8:00:00 a.m.</td>
            <td>9:00:00 a.m.</td>
		</tr>
         <tr>			
			<td>27 ms</td>
			<td>50.66 ms</td>
			<td>8:00:00 a.m.</td>
            <td>9:00:00 a.m.</td>
		</tr>
         <tr>			
			<td>27 ms</td>
			<td>50.66 ms</td>
			<td>8:00:00 a.m.</td>
            <td>9:00:00 a.m.</td>
		</tr>
	</tbody>
</table>
 </div>



    </form>

    <div class="row">
        <div class="col-sm-10"></div>
    <div class="col-sm-2">
        <button type="button" class="btn btn-primary">Regresar</button>
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

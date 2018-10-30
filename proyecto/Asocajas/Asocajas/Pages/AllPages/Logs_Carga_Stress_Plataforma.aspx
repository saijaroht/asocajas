<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/AllPages/pages.Master" AutoEventWireup="true" CodeBehind="Logs_Carga_Stress_Plataforma.aspx.cs" Inherits="Asocajas.Pages.AllPages.Logs_Carga_Stress_Plataforma" %>
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
    <%--<ul class="breadcrumb">
        <li><a href="Inicio.aspx">Administración</a></li>
        <li class="active">Logs Desempeño Plataforma</li>
    </ul>--%>
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
			<th class="text-center"data-field="ccf" data-filter-control="select" data-sortable="true">Picos de consumo</th>
			<th class="text-center"data-field="user" data-filter-control="select" data-sortable="true">Volumen de consultas</th>
            <th class="text-center"data-field="fechaIni" data-filter-control="input" data-sortable="true">Volumen usuarios Conectados</th>
           		
		
		</tr>
	</thead>
    <tbody id="tbody">
        <tr>			
			<td>Cell 1</td>
			<td>Cell 2</td>
			<td>Cell 3</td>
			
		</tr>

           <tr>			
			<td>Cell 4</td>
			<td>Cell 5</td>
			<td>Cell 6</td>
			
		</tr>

           <tr>			
			<td>Cell 7</td>
			<td>Cell 8</td>
			<td>Cell 9</td>
			
		</tr>

           <tr>			
			<td>Cell 10</td>
			<td>Cell 11</td>
			<td>Cell 12</td>
			
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

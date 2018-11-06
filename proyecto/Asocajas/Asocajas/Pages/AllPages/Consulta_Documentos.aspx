<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/AllPages/pages.Master" AutoEventWireup="true" CodeBehind="Consulta_Documentos.aspx.cs" Inherits="Asocajas.Pages.AllPages.Consulta_Documentos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../../Scripts/Javascript/Consulta_Documentos.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TitlePage" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="bodyContent" runat="server">
    <%--<ul class="breadcrumb">
        <li><a href="Inicio.aspx">Administración</a></li>
        <li class="active">Consulta Documentos</li>
    </ul>--%>
	<h4 class="text-center "><b>Consulta Individual Documentos</b></h4>
	
  <form>	
	<div class="encabezadoTable">
        <div class="col-xs-12 col-sm-12 col-md-12">	
			<div class="col-md-3"></div>
            <div class="col-xs-8 col-sm-8 col-md-3 ">
				<div class="col-xs-12 col-sm-12 col-md-12 txtEncabezadoLog">
				  <span>Documento</span>
				</div>
				<div class="col-xs-12 col-sm-12 col-md-12" >
				<input id="Number1" type="number" placeholder="Número Identificación" class="form-control" required onKeyDown="if(this.value.length>9 && event.keyCode!=8) return false;">			
				</div>
			</div>
			<div class="col-md-1"></div>
            <div class="col-xs-4 col-sm-4 col-md-2 nuevoUser">					
                <button type="button" class="btn btn-success" id="btnConsultar" onclick="alerta();" >Consultar</button>
            </div>
			
        </div>
			
	</div>
	<div class="row">
	  <h5 class="text-center "><b>Para la consulta debe tener en cuenta que: Sólo se puede ingresar un documento a la vez, sólo se permite digitar números, el documento ingresado no puede tener más de diez (10) digitos </b></h5>
	</div>
	<!--
    <div class="row">
        <div class="col-sm-3">
             <h4><strong>Consulta Documentos</strong></h4>
        </div>
        <div class="col-sm-1"></div>
        <div class="col-sm-4">
            <div class="input-group">
				<span class="input-group-addon"><b>Identificación</b></span>
				<input id="Number1" type="number" class="form-control" required onKeyDown="if(this.value.length>9 && event.keyCode!=8) return false;">
			</div>
        </div>
       
        <div class="col-sm-2"></div>
		<div class="col-sm-2">
            <button type="button" class="btn btn-primary">Consultar</button>
        </div>



     
    </div>

    <br/>
    <br/>
-->

<!--
    
 </form>		
  <!--
    <div class="row">
        <div class="col-sm-10" ></div>
        <div class="col-sm-2" >
        <button type="button" class="btn btn-primary" onclick="">Regresar</button>
            </div>
    </div>
    -->
   

    <div class="modal fade" id="ModalConsultarUsuarioDocumento" role="dialog">
        <div class="modal-dialog modal-lg">
            <!-- Modal content-->
            <div class="modal-content">
			<!--
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Formulario</h4>
                </div>
				-->
                <div class="modal-body">

                  <div class="row">
         <div class="col-sm-2"></div>
        
        <div class="col-sm-8">
    <table class="table table-bordered bordertable">

        <tbody class="bordertable">
            <tr>
                <th scope="row" class="thVertical">Identificación</th>
                <td>123456789</td>
            </tr>

            <tr>
                <th scope="row" class="thVertical">Primer Apellido</th>
                <td>Suarez</td>
            </tr>

            <tr>
                <th scope="row" class="thVertical">Particula</th>
                <td>Sin Particula</td>
            </tr>

            <tr>
                <th scope="row" class="thVertical">Segundo Apellido</th>
                <td>Gamboa</td>
            </tr>

            <tr>
                <th scope="row" class="thVertical">Primer Nombre</th>
                <td>Adriana</td>
            </tr>

            <tr>
                <th scope="row" class="thVertical">Segundo Nombre</th>
                <td>Marcela</td>
            </tr>

            <tr>
                <th scope="row" class="thVertical">Fecha de Nacimiento</th>
                <td>10/06/1980</td>
            </tr>

            <tr>
                <th scope="row" class="thVertical">Municipio de Expedición</th>
                <td>Bogotá D.C.</td>
            </tr>

            <tr>
                <th scope="row" class="thVertical">Departamento de Expedición</th>
                <td>Distrito Capital</td>
            </tr>

            <tr>
                <th scope="row" class="thVertical">Fecha de Expedición</th>
                <td>06/08/1998</td>
            </tr>

            <tr>
                <th scope="row" class="thVertical">Estado del documento</th>
                <td>0</td>
            </tr>

            <tr>
                <th scope="row" class="thVertical">Número de Resolución</th>
                <td>2355</td>
            </tr>

            <tr>
                <th scope="row" class="thVertical">Año de Resolución</th>
                <td>2007</td>
            </tr>
        </tbody>
    </table>
            </div>
        </div>

                    <div class="group">
						<div class="col-md-3"></div>
						<div class="col-md-2">
							<button type="button" class="btn btn-primary" data-dismiss="modal">Cancelar</button>
						</div>						
                        <div class="col-md-2"></div>
                        <div class="col-md-2">
                            <%--<button type="button" class="btn btn-primary" onclick="ValidaUsuario();">Guardar</button>--%>
                        </div>
						<div class="col-md-3"></div>
                    </div>
                </div>
                <div class="modal-footer">
                    <div class="col-md-9"></div>
                    <div class="col-md-3">
                    </div>
                </div>
            </div>

        </div>
    </div>

</asp:Content>
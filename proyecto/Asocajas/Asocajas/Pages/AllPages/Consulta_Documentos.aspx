﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/AllPages/pages.Master" AutoEventWireup="true" CodeBehind="Consulta_Documentos.aspx.cs" Inherits="Asocajas.Pages.AllPages.Consulta_Documentos" %>
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
				<input id="Identificacion" type="text" maxlength="10" placeholder="Número Identificación" class="form-control" required onkeypress="if ((event.keyCode < 48 || event.keyCode > 57) && event.keyCode != 13 && event.keyCode != 8 && event.keyCode != 0) return false;">			
				</div>
			</div>
			<div class="col-md-1"></div>
            <div class="col-xs-4 col-sm-4 col-md-2 nuevoUser">					
                <button type="button" class="btn btn-success" id="btnConsultar" onclick="consultaDocumento();" >Consultar</button>
            </div>
			
        </div>
			
	</div>
	<div class="row col-md-6 col-md-offset-3">
	  <h3 class="text-justify "><b>Para la consulta debe tener en cuenta que:  </b></h3>
	  
	  <ul>
		<li type="circle"><h4 class="text-justify "><b>Sólo se puede ingresar un documento a la vez. </b></h4></li>
		<li type="circle"><h4 class="text-justify "><b>Sólo se permite digitar números. </b></h4></li>
		<li type="circle"><h4 class="text-justify "><b>El documento ingresado no puede tener más de diez (10) digitos. </b></h4></li>	  
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
                <td ><input class="form-control styleinput" id="txtnuip" type="text" disabled></td>
            </tr>
              <tr>
                <th scope="row" class="thVertical">Particula</th>
                <td><input class="form-control styleinput" id="txtparticula" type="text" disabled></td>
            </tr>

            <tr>
                <th scope="row" class="thVertical">Primer Apellido</th>
                <td><input class="form-control styleinput" id="txtprimerApellido" type="text" disabled></td>
            </tr>



            <tr>
                <th scope="row" class="thVertical">Segundo Apellido</th>
                <td><input class="form-control styleinput" id="txtsegundoApellido" type="text" disabled ></td>
            </tr>

            <tr>
                <th scope="row" class="thVertical">Primer Nombre</th>
                <td><input class="form-control styleinput" id="txtprimerNombre" type="text" disabled ></td>
            </tr>

            <tr>
                <th scope="row" class="thVertical">Segundo Nombre</th>
                <td><input class="form-control styleinput" id="txtsegundoNombre" type="text"disabled ></td>
            </tr>

            <tr>
                <th scope="row" class="thVertical">Fecha de Nacimiento</th>
                <td><input class="form-control styleinput" id="txtfechaNacimiento" type="text" disabled ></td>
            </tr>

            <tr>
                <th scope="row" class="thVertical">Municipio de Expedición</th>
                <td><input class="form-control styleinput" id="txtmunicipioExpedicion" type="text" disabled></td>
            </tr>

            <tr>
                <th scope="row" class="thVertical">Departamento de Expedición</th>
                <td><input class="form-control styleinput" id="txtdepartamentoExpedicion" type="text" disabled></td>
            </tr>

            <tr>
                <th scope="row" class="thVertical">Fecha de Expedición</th>
                <td><input class="form-control styleinput" id="txtfechaExpedicion" type="text" disabled></td>
            </tr>

            <tr>
                <th scope="row" class="thVertical">Estado del documento</th>
                <td><input class="form-control styleinput" id="txtestadoCedula" type="text" disabled></td>
            </tr>

            <tr>
                <th scope="row" class="thVertical">Número de Resolución</th>
                <td><input class="form-control styleinput" id="txtnumResolucion" type="text" disabled></td>
            </tr>

            <tr>
                <th scope="row" class="thVertical">Año de Resolución</th>
                <td><input class="form-control styleinput" id="txtanoResolucion" type="text" disabled></td>
            </tr>
        </tbody>
    </table>
            </div>
        </div>

                    <div class="group">
						<div class="col-md-5"></div>
						<div class="col-md-2">
							<button type="button" class="btn btn-primary" data-dismiss="modal">Cerrar</button>
						</div>						
                        <div class="col-md-2"></div>
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
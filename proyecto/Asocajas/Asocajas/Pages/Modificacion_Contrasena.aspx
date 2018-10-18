<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Login.Master" AutoEventWireup="true" CodeBehind="Modificacion_Contrasena.aspx.cs" Inherits="Asocajas.Pages.Modificacion_Contraseña" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../Scripts/Javascript/Modificacion_Contrasena.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TitlePage" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentBody" runat="server">

    <!--<div class="row">-->
	<div class="w3l-login-form col-md-6 col-ms-6 col-xs-12 centred">
            <div class="w3l-form-group">
                <div class="imgbanner">
                    <img src="../Scripts/Images/logo_asocajas.png" class="img-responsive"></div>
                <h4 class="text-center titleLoguin"><b>Servicio Digital de Validación de Identidad (ANI)</b></h4>
            </div>
        
        <div class="w3l-form-group">
		<h2>Recuperar Clave</h2>
        <p>Desde esta pantalla podrá solicitar la información de su clave</p>
        <p>Digite su usuario y se le enviara un mensaje a su correo electrónico con instrucciones para restablecer su clave.</p>

            <div class="group">
                <i class="fas fa-user"></i>
                <input type="text" class="form-control" placeholder="Usuario" required="required" id="txtContrasena" />
            </div>

            <div class="group">
				<div class="col-md-2"></div>
                <div class="col-md-3">
                    <button type="button" class="btn btn-primary" onclick="cancelar();">Volver</button>
                </div>

                 <div class="col-md-2"></div>
                <div class="col-md-3">
                    <button type="button" class="btn btn-primary" onclick="oprimirbtn()">Solicitar</button>
                </div>
				<div class="col-md-2"></div>
            </div>
		</div>
    </div>
	  <div class="copytxt" align="center">

          <h5>© 2019 ASOCAJAS. Todos los derechos reservados</h5>
      </div>

    



</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Login.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Asocajas.Pages.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        footer {
            position: absolute;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TitlePage" runat="server">
    <%-- <h1 class="text-center titleLoguin">Servicio digital de validacion de identidad</h1>--%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentBody" runat="server">

    <script src="../Scripts/Javascript/Login.js" type="text/javascript"></script>
    <script src="https://www.google.com/recaptcha/api.js?onload=recaptchaCallback&render=explicit" async defer></script>
    <%--<script src='https://www.google.com/recaptcha/api.js?hl=es'></script>--%>

    <div class="w3l-login-form col-md-4 col-ms-4 col-xs-12 centred">
        <form>
            <div class="w3l-form-group">
                <div class="imgbanner">
                    <img src="../Scripts/Images/logo_asocajas.png" class="img-responsive"></div>
                <h5 class="text-center titleLoguin"><b>Servicio Digital de Validación de Identidad (ANI)</b></h5>
            </div>
            <div>
                <div >
                    <div class="group">
                        <i class="fas fa-user"></i>
                        <input type="email" class="form-control" placeholder="Usuario" required="required" id="txtUsuario" />						
                    </div>
					<div>
						<label id="lblvalidaciontxtusuario" style="display: none; color: red">Correo Electronico no valido!</label>
					</div>
                </div>
                <div >
                    <div class="group">
                        <i class="fas fa-unlock"></i>
                        <input type="password" class="form-control" placeholder="Clave" required="required" id="txtContrasena" />
                    </div>
                </div>
                <div class="forgot">
                    <a href="Modificacion_Contrasena.aspx"><u>Olvidé mi contraseña<u></a>
                </div>
                <div class=" w3l-form-group">
                    <div class="col-xs-10 col-sm-8 col-md-12 centred">
                       <label for="" class="labelText">Captcha</label>
                        <div id="recaptcha"></div>
                        <!-- <input type="text" id="txtcaptcha" style="display:none;color:red" />-->
                        <label id="lblvalidacioncaptcha" style="display: none; color: red">El Captcha es requerido!</label>
                        <label id="lblvalidacioncaptchaok" style="display: none; color: green">Captcha completado! </label>
                    </div>
                    <%--<div class="g-recaptcha" data-sitekey="6Lcum3UUAAAAAEP7sz4o9d7hy2T7Vbjp-EKiTJXF" align="center"></div>--%>
                </div>
				
                <div class="col-md-6 col-sm-6 col-xs-6 centred">
					<button class="hoverbtn"  style="margin-top: 15px" type="button" onclick="ValidaUsuario(); return false;">Ingresar</button>                    
                </div>
				
			</div>
        </form>

    </div>
	  <div class="copytxt" align="center">

          <h5 style="margin-top: 30px" >© 2019 ASOCAJAS. Todos los derechos reservados</h5>
      </div>

</asp:Content>
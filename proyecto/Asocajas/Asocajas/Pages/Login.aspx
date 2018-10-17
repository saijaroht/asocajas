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
    <script src='https://www.google.com/recaptcha/api.js?hl=es'></script>

    <div class="w3l-login-form col-md-4 col-ms-4 col-xs-12 centred">
        <form>
            <div class="w3l-form-group">
                <div class="imgbanner">
                    <img src="../Scripts/Images/logo_asocajas.png" class="img-responsive"></div>
                <h4 class="text-center titleLoguin"><b>Servicio Digital de Validación de Identidad (ANI)</b></h4>
            </div>
            <div class="">
                <div class="w3l-form-group">
                    <div class="group">
                        <i class="fas fa-user"></i>
                        <input type="email" class="form-control" placeholder="Usuario" required="required" id="txtUsuario" />
                    </div>
                </div>
                <div class=" w3l-form-group">
                    <div class="group">
                        <i class="fas fa-unlock"></i>
                        <input type="password" class="form-control" placeholder="Clave" required="required" id="txtContrasena" />
                    </div>
                </div>
                <div class="forgot">
                    <a href="Modificacion_Contrasena.aspx"><u>Olvide mi contraseña<u></a>
                </div>
                <div class=" w3l-form-group">
                    <div class="g-recaptcha" data-sitekey="6LcBs3QUAAAAAON6l4A3w66mtziMtI3YyoWGtECD" align="center"></div>
                </div>
                <div class=" w3l-form-group">

                    <!--<div class="recaptcha"></div>
                <div class="g-recaptcha" data-sitekey="6Lc9FXQUAAAAAAD9ZwF-PjD0e10HdtZDnJJQPFp0"></div> -->
                </div>
                <br />
                <br />
                <br />
                <div class="group">
                    <button type="submit" onclick="ValidaUsuario(); return false;">Iniciar sesión</button>
                </div>
        </form>

    </div>
    </div>
	  <div class="copytxt" align="center">

          <span>Copyright © 2019 ASOCAJAS. Todos los derechos reservados Ver. 1.0</span>
      </div>

</asp:Content>

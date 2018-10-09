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
    <%--<script src="https://www.google.com/recaptcha/api.js" ></script>--%>

    
    <!--<form>
  <div class="input-group">
    <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
    <input id="email" type="text" class="form-control" name="email" placeholder="usuario@correo.com">
  </div>
       <br />
  <div class="input-group">
    <span class="input-group-addon"><i class="glyphicon glyphicon-lock"></i></span>
    <input id="password" type="password" class="form-control" name="password" placeholder="Clave">
  </div>
        <a href"#">Olvide mi contraseña</a>
        <br />
        <br />
        <div class="row">
                    <div class="col-xs-12 col-sm-12 col-md-12">
                       <label for="" class="labelText">Captcha</label>
                        <div id="recaptcha"></div>
                        <!-- <input type="text" id="txtcaptcha" style="display:none;color:red" />
                        <label id="lblvalidacioncaptcha" style="display: none; color: red">El Captcha es requerido!</label>
                        <label id="lblvalidacioncaptchaok" style="display: none; color: green">Captcha completado! </label>
                    </div>
                   
                </div>
  
</form>-->
   
   

        <div class="w3l-login-form col-md-4 col-ms-4 col-xs-12 centred">       
        <form>
            <div class="">
            <div class="w3l-form-group">
                <div class="group">
                    <i class="fas fa-user"></i>
                    <input type="email" class="form-control" placeholder="Usuario@ejemplo.com" required="required" id="txtUsuario" />
                </div>
            </div>
            <div class=" w3l-form-group">          
                <div class="group">
                    <i class="fas fa-unlock"></i>
                    <input type="password" class="form-control" placeholder="Contraseña" required="required" id="txtContrasena" />
                </div>
            </div>
            <div class="forgot">
                <a href="#">Olvide mi contraseña</a>                
            </div>
            <div class=" w3l-form-group">          
                <%--<div class="recaptcha"></div>--%>
                <%--<div class="g-recaptcha" data-sitekey="6Lc9FXQUAAAAAAD9ZwF-PjD0e10HdtZDnJJQPFp0"></div>--%>
            </div>
            <button type="submit" onclick="ValidaUsuario(); return false;">Iniciar sesión</button>
           </div>
        </form>
        
    </div>
   
</asp:Content>

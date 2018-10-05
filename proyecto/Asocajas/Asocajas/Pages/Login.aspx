<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Login.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Asocajas.Pages.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TitlePage" runat="server">
    <h1 class="text-center">Servicio digital de validacion de identidad</h1>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentBody" runat="server">
    <form>
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
                        <!-- <input type="text" id="txtcaptcha" style="display:none;color:red" />-->
                        <label id="lblvalidacioncaptcha" style="display: none; color: red">El Captcha es requerido!</label>
                        <label id="lblvalidacioncaptchaok" style="display: none; color: green">Captcha completado! </label>
                    </div>
                   
                </div>
  
</form>
</asp:Content>

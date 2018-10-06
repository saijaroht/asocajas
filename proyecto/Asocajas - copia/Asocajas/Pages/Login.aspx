<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Login.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Asocajas.Pages.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TitlePage" runat="server">
   <%-- <h1 class="text-center titleLoguin">Servicio digital de validacion de identidad</h1>--%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentBody" runat="server">

  
    
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
  
</form>-->
   
   

        <div class="w3l-login-form col-md-4 col-ms-4 col-xs-12 centred">       
        <form>
            <div class="">
            <div class="w3l-form-group">
        
                <div class="group">
                    <i class="fas fa-user"></i>
                    <input type="text" class="form-control" placeholder="Usuario" required="required" />
                </div>
            </div>
            <div class=" w3l-form-group">          
                <div class="group">
                    <i class="fas fa-unlock"></i>
                    <input type="email" class="form-control" placeholder="Correo" required="required" />
                </div>
            </div>
            <div class="forgot">
                <a href="#">Olvide mi contraseña</a>                
            </div>
            <button type="submit">Iniciar sesión</button>
                <div class="g-recaptcha" data-sitekey="6Ld9pHMUAAAAAG4AuiALeiKAPgNkNxgae6QR-otq"></div>
           </div>
        </form>
        
    </div>
   
</asp:Content>

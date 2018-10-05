<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Login.Master" AutoEventWireup="true" CodeBehind="RecordarClave.aspx.cs" Inherits="Asocajas.Pages.RecordarClave" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TitlePage" runat="server">
</asp:Content>
 <script src="../Scripts/Javascript/RecordarClave.js" type="text/javascript"></script>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentBody" runat="server">
</asp:Content>
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
  
</form>
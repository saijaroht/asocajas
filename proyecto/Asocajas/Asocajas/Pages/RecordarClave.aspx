<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Login.Master" AutoEventWireup="true" CodeBehind="RecordarClave.aspx.cs" Inherits="Asocajas.Pages.RecordarClave" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../Scripts/Javascript/RecordarClave.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TitlePage" runat="server">
    
   <u><center><b> Servicio digital de validación de identidad</b></center></u>
    <br>
    
            <H4><center>Desde esta pantalla podra solicitar la modificación de la contraseña.<br><br>
              Introducciendo el login de ususario se enviará un mensaje a su cuenta de correo.</center>
            </H4>
        
</asp:Content> 
<asp:Content ID="Content3" ContentPlaceHolderID="ContentBody" runat="server">
    <form>
         <div class="panel panel-default">
            <div class="panel-body form-horizontal payment-form"> 
          <div class="input-group">
                
                    <span class="input-group-addon"><i class="glyphicon glyphicon-user">
                    
                                                    </i></span>
                    <input id="email" type="text" class="form-control" name="email" placeholder="usuario@correo.com">
                  </div>
                       <br />
  
           
                </div>
             </div>
         <a href="#" class="btn btn-primary btn-lg active" role="button">Volver</a>
                  <a href="#" class="btn btn-primary btn-lg active" role="button">Solicitar</a>
</form>
</asp:Content>

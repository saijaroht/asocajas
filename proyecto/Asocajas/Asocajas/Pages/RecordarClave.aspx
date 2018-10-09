<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Login.Master" AutoEventWireup="true" CodeBehind="RecordarClave.aspx.cs" Inherits="Asocajas.Pages.RecordarClave" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../Scripts/Javascript/RecordarClave.js" type="text/javascript"></script>
        <style>

        
footer {  
    position: absolute;    
}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TitlePage" runat="server">
    
   <u><center><b> Servicio digital de validación de identidad</b></center></u>
    <br>
    
            <H4><center>Desde esta pantalla podra solicitar la modificación de la contraseña.<br><br>
              Introducciendo el login de ususario se enviará un mensaje a su cuenta de correo.</center>
            </H4>
        
</asp:Content> 
<asp:Content ID="Content3" ContentPlaceHolderID="ContentBody" runat="server">
   <%-- <form>
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
        
</form>--%>
    <div class="RecordarContrasena">
     <div class="col-sm-2"></div>
        <div class="col-sm-8">
            <div class="panel panel-primary">
                <form class="form-horizontal">
                    <div class="panel-heading">Recuperar Clave</div>
                     <div class="panel-body">
                           <div class="form-horizontal">
                            <div class="input-group">                
                                <span class="input-group-addon"><i class="glyphicon glyphicon-user">
                                    </i></span>
                                <input id="email" type="text" class="form-control" name="email" placeholder="usuario@correo.com">
                                </div>               
                            </div>
                         <div class="row" id="btninfo">
                             <div class="col-xs-12 col-sm-3 col-md-3">
                                </div>
                             <div class="col-xs-12 col-sm-3 col-md-3">
                                 <div class="centred">
                                        <a href="#" class="btn btn-primary  active" role="button">Volver</a>
                                     </div>
                             </div>
                          <div class="col-xs-12 col-sm-3 col-md-3">
                              <div class="centred">
                                    <a href="#" class="btn btn-primary active" role="button">Solicitar</a>
                             </div>
                         </div>
                             <div class="col-xs-12 col-sm-3 col-md-3">
                                </div>
                       </div>
                         </div>
                         
                     </div>
                </form>

                
            </div>
        </div>
        <div class="col-sm-2"></div>
     </div>
</asp:Content>

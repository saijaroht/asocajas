<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/AllPages/pages.Master" AutoEventWireup="true" CodeBehind="Cambio_Clave.aspx.cs" Inherits="Asocajas.Pages.AllPages.Cambio_Clave" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TitlePage" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="bodyContent" runat="server">




    <div class="row">
        <div class="col-sm-1"></div>
        <div class="col-sm-7">
            <h4><b>Cambiar Contraseña</b></h4>
            <br />
            <p> Es consultante y es la primera vez que se loguea. Por razonez de seguridad por favor cambie su contraseña.</p>
        </div>
        <div class="col-sm-4"></div>
    </div>

    <div class="row">
        <div class="col-sm-1"></div>
        <div class="col-sm-7">
            <div class="row">
            <div class="col-sm-6 BordeDiv">
                <div class="form-group">
                    <label >Contraseña Actual</label>
                    <input type="password" class="form-control" id="txtContrasenaActual">
                </div>

                 <div class="form-group">
                    <label>Nueva Contraseña</label>
                    <input type="password" class="form-control" id="txtNuevaContraseña">
                </div>

                 <div class="form-group">
                    <label >Confirmar Contraseña</label>
                    <input type="password" class="form-control" id="txtConfirmarContraseña">
                </div>
            </div>
             
            <div class="col-sm-6 BordeDiv">
                <h5 class="text-center"><b>Requerimientos Mínimos</b></h5>
                <ul>
                    <li>Minimo 8 caracteres</li>
                    <li>Debe contener los siguientes Items:</li>

                    <ul>
                        <li>Al menos una letra mayúscula</li>
                        <li>Al menos una letra minúscula</li>
                        <li>Números</li>
                        <li>Caracter especial</li>
                    </ul>
                </ul>
            </div>

                
            </div>
          
        </div>

        
        <div class="col-sm-1">
            </div>
        
        <div class="col-sm-2">
            <button type="button" class="btn btn-primary">Aceptar</button>
        </div>
    </div>



  
</asp:Content>



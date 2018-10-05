<%@ Page Title="" Language="C#"MasterPageFile="~/Pages/AllPages/pages.Master" AutoEventWireup="true" CodeBehind="RegistroUsuarios.aspx.cs" Inherits="Asocajas.Pages.RegistroUsuarios" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TitlePage" runat="server">
    <h4 class="text-center"><u><b>Servicio Digital de Validacion de Identidad</b></u></h4>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="bodyContent" runat="server">
     <form>
  <div class="panel panel-default">
            <div class="panel-body form-horizontal payment-form">   
              <div class="input-group">
                <span class="input-group-addon">Nombre<i class=""></i></span>
                <input id="txtNombre" type="text" class="form-control" name="Nombre" placeholder="Nombre">
              </div>
                   <br />
                 <div class="input-group">
                <span class="input-group-addon">Apellido<i class=""></i></span>
                <input id="txtApellido" type="text" class="form-control" name="Apellido" placeholder="Apellido">
              </div>
                   <br />
                 <div class="input-group">
                <span class="input-group-addon">usuario<i class=""></i></span>
                <input id="txtUsuario" type="text" class="form-control" name="Usuario" placeholder="Usuario">
              </div>
                   <br />
                <div class="input-group">
                    <span class="input-group-addon">Nombre CCF<i class=""></i></span>
                    <div class="col-sm-9">
                        <select id="ddCCF" class="form-control">
                        </select>
                    </div>
                </div>
                   <br />
                <div class="input-group">
                    <span class="input-group-addon">Estado<i class=""></i></span>
                    <div class="col-sm-9">
                        <select id="ddEstado" class="form-control">
                        </select>
                    </div>
                </div>
                   <br />
                <div class="input-group">
                    <span class="input-group-addon">Tipo de usuario<i class=""></i></span>
                    <div class="col-sm-9">
                        <select id="ddTipoUsuario" class="form-control">
                        </select>
                    </div>
                </div>
                   <br />
                 
               <div class="form-group">
                <label>Fecha Desde:</label>
                <input type="text" placeholder="DD/MM/YYYY" class="form-control" name="txtFechaInicioRecordatorio" id="txtFechaInicioRecordatorio">
              </div>
              
                   
  </div>
      </div>
</form>
</asp:Content>

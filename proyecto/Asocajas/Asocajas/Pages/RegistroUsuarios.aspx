<%@ Page Title="" Language="C#"MasterPageFile="~/Pages/AllPages/pages.Master" AutoEventWireup="true" CodeBehind="RegistroUsuarios.aspx.cs" Inherits="Asocajas.Pages.RegistroUsuarios" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../Scripts/Javascript/RegistroUsuarios.js"type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TitlePage" runat="server">
    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="bodyContent" runat="server">
    <div class="row">
        <div class="col-sm-2"></div>
        <div class="col-sm-8">
            <div class="panel panel-primary">
                <form class="form-horizontal">
                    <div class="panel-heading">Registro Usarios</div>
                     <div class="panel-body">

                   <div class="form-horizontal">
                      <div class="form-group">
                        <label for="" class="col-sm-3 control-label">Nombres:</label>
                        <div class="col-sm-9">
                            <input class="form-control" id="txtNombres" type="text" placeholder="Nombres">                         
                        </div>
                      </div>
                      <div class="form-group">
                        <label for="" class="col-sm-3 control-label">Apellidos:</label>
                        <div class="col-sm-9">
                          <input class="form-control" id="txtApellidos" type="text" placeholder="Apellidos">
                        </div>
                      </div>

                      <div class="form-group">
                        <label for="" class="col-sm-3 control-label">Usuario:</label>
                        <div class="col-sm-9">
                          <input class="form-control" id="txtUsuario" type="text" placeholder="Usuario">
                        </div>
                      </div>

                      <div class="form-group">
                        <label for="" class="col-sm-3 control-label">Nombre CCF:</label>
                        <div class="col-sm-9">
                          <input class="form-control" id="txtNombreCCF" type="text" placeholder="Nombre CCF">
                        </div>
                      </div>

                     <div class="form-group">
                        <label for="s" class="col-sm-3 control-label">Estado:</label>
                        <div class="col-sm-9">
                          <select class="form-control" name="cboEstado" id="cboEstado">
                                <option value="">Seleccione...</option>
                            </select>
                        </div>
                      </div>
                      <div class="form-group">
                        <label for="" class="col-sm-3 control-label">Tipo de usuario:</label>
                        <div class="col-sm-9">
                          <select class="form-control" name="cboTipodeusuario" id="cboTipodeusuario">
                                <option value="">Seleccione...</option>
                            </select>
                        </div>
                      </div>

                     <div class="form-group">
                        <label for="" class="col-sm-3 control-label">Fecha de caducidad:</label>
                        <div class="col-sm-9">
                          <div class="input-group">
                                 <input type="text" class="form-control" datepicker-popup="yyyy/mm/dd" name="txtFechadecaducidad" id="txtFechadecaducidad" placeholder="DD/MM/YYYY" datepicker required>
                                <div class="input-group-addon">
                                <span class="glyphicon glyphicon-calendar"></span>
                            </div>
                            </div>
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

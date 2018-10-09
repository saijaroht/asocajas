<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/AllPages/pages.Master" AutoEventWireup="true" CodeBehind="Registro_usuarios.aspx.cs" Inherits="Asocajas.Pages.AllPages.Registro_usuarios" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../../Scripts/Javascript/RegistroUsuarios.js" type="text/javascript"></script>
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
                                <label class="col-sm-4 control-label styleLabel">Nombres:</label>
                                <div class="col-sm-8">
                                    <input class="form-control styleinput" id="txtNombres" type="text" placeholder="Nombres">
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-4 control-label styleLabel">Apellidos:</label>
                                <div class="col-sm-8">
                                    <input class="form-control styleinput styleLabel" id="txtApellidos" type="text" placeholder="Apellidos">
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-4 control-label styleLabel">Usuario:</label>
                                <div class="col-sm-8">
                                    <input class="form-control styleinput" id="txtUsuario" type="email" placeholder="Usuario">
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-4 control-label styleLabel">Nombre CCF:</label>
                                <div class="col-sm-8">
                                    <select class="form-control styleinput" name="cboNombreCCF" id="cboNombreCCF">
                                    </select>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-4 control-label styleLabel">Estado:</label>
                                <div class="col-sm-8">

                                    <select class="form-control styleinput" name="cboEstado" id="cboEstado" disabled>
                                        <option value="1">Activo</option>
                                    </select>

                                </div>
                            </div>

                            <div class="form-group">
                                <label class="col-sm-4 control-label styleLabel">Tipo de usuario:</label>
                                <div class="col-sm-8">
                                    <select class="form-control styleinput" name="cboTipodeusuario" id="cboTipodeusuario">
                                    </select>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-4 control-label styleLabel">Fecha de caducidad:</label>
                                <div class="col-sm-8">
                                    <div class='input-group date' id='datetimepicker1'>
                                        <input type='text' class="form-control styleinput" datepicker-popup="yyyy/mm/dd" name="txtFechadecaducidad" id="txtFechadecaducidad" placeholder="DD/MM/YYYY" datepicker required />
                                        <span class="input-group-addon">
                                            <span class="glyphicon glyphicon-calendar"></span>
                                        </span>
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
    <div class="row">
        <div class="col-sm-2">
            <button type="button" class="btn btn-primary">Cancelar</button>
        </div>
        <div class="col-sm-8"></div>
        <div class="col-sm-2">
            <button type="button" class="btn btn-primary" onclick="ValidaUsuario();">Guardar</button>
        </div>
    </div>
    <br />
    <br />
</asp:Content>

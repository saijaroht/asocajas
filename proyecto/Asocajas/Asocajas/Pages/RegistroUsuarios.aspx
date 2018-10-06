<%@ Page Title="" Language="C#"MasterPageFile="~/Pages/AllPages/pages.Master" AutoEventWireup="true" CodeBehind="RegistroUsuarios.aspx.cs" Inherits="Asocajas.Pages.RegistroUsuarios" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../Scripts/Javascript/RegistroUsuarios.js"type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TitlePage" runat="server">
    <h4 class="text-center"><u><b>Servicio Digital de Validacion de Identidad</b></u></h4>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="bodyContent" runat="server">
    <div class="row">
        <div class="col-sm-2"></div>
        <div class="col-sm-8">
            <div class="panel panel-default panelPadding">
                <form class="form-horizontal">

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
                            <input class="form-control styleinput" id="txtUsuario" type="text" placeholder="Usuario">
                        </div>
                    </div>

                    <div class="form-group">
                        <label class="col-sm-4 control-label styleLabel">Nombre CCF:</label>
                        <div class="col-sm-8">
                            <input class="form-control styleinput" id="txtNombreCCF" type="text" placeholder="Nombre CCF">
                        </div>
                    </div>

                     <div class="form-group">
                        <label class="col-sm-4 control-label styleLabel">Estado:</label>
                        <div class="col-sm-8">
                            <input class="form-control styleinput" id="txtEstado" type="text" placeholder="Estado">
                        </div>
                    </div>

                    <div class="form-group">
                        <label class="col-sm-4 control-label styleLabel">Tipo de usuario:</label>
                        <div class="col-sm-8">
                            <select class="form-control styleinput" name="cboTipodeusuario" id="cboTipodeusuario">
                                <option value="">Seleccione...</option>
                            </select>
                        </div>
                    </div>

                    <div class="form-group">
                        <label class="col-sm-4 control-label styleLabel">Fecha de caducidad:</label>
                        <div class="col-sm-8">
                            <input type="text" class="form-control styleinput" datepicker-popup="yyyy/mm/dd" name="txtFechadecaducidad" id="txtFechadecaducidad" placeholder="DD/MM/YYYY" datepicker required>
                            <div class="input-group-addon">
                                <span class="glyphicon glyphicon-calendar"></span>
                            </div>
                        </div>
                    </div>

                </form>
            </div>
        </div>
        <div class="col-sm-2"></div>
    </div>

    <div class="container">
    <div class="row">
        <div class='col-sm-6'>
            <div class="form-group">
                <div class='input-group date' id='datetimepicker1'>
                    <input type='text' class="form-control" />
                    <span class="input-group-addon">
                        <span class="glyphicon glyphicon-calendar"></span>
                    </span>
                </div>
            </div>
        </div>
        <script type="text/javascript">
            $(function () {
                $('#datetimepicker1').datetimepicker();
            });
        </script>
    </div>
</div>
</asp:Content>

﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/AllPages/pages.Master" AutoEventWireup="true" CodeBehind="Registro_usuarios.aspx.cs" Inherits="Asocajas.Pages.AllPages.Registro_usuarios" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TitlePage" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="bodyContent" runat="server">
    <br/>
    <br/>
    <br />

    <div class="row">
        <div class="col-sm-3"></div>
        <div class="col-sm-6">
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
                          
                            <select class="form-control styleinput" name="cboEstado" id="cboEstado">
                                <option value="">Seleccione...</option>
                            </select>
                        
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
                        <div class='input-group date' id='datetimepicker1'>
                            <input type='text' class="form-control styleinput" datepicker-popup="yyyy/mm/dd" name="txtFechadecaducidad" id="txtFechadecaducidad" placeholder="DD/MM/YYYY" datepicker required />
                            <span class="input-group-addon">
                                <span class="glyphicon glyphicon-calendar"></span>
                            </span>
                        </div>
                    </div>
                </div>
            </form>
        </div>
        <div class="col-sm-3"></div>
    </div>
    <br />
    <br />
    <br />
    <br />

    <div class="row">
        <div class="col-sm-2">
            <button type="button" class="btn btn-primary">Cancelar</button>
        </div>
        <div class="col-sm-8"></div>
          <div class="col-sm-2">
              <button type="button" class="btn btn-primary">Guardar</button>
           </div>
    </div>

    <br/>
    <br/>


    
</asp:Content>

﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/AllPages/pages.Master" AutoEventWireup="true" CodeBehind="Gestion_Usuarios.aspx.cs" Inherits="Asocajas.Pages.AllPages.Gestion_Usuarios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../../Scripts/Javascript/Gestion_Usuarios.js" type="text/javascript"></script>
    <script src="../../Scripts/Javascript/RegistroUsuarios.js" type="text/javascript"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TitlePage" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="bodyContent" runat="server">

    <%--<ul class="breadcrumb">
        <li><a href="Inicio.aspx">Administración</a></li>
        <li class="active">Gestión de Usuarios</li>
    </ul>--%>
    <form>
        <div class="encabezadoTable">
            <div class="col-xs-12 col-sm-12 col-md-6">
                <div class="textEncabezado">
                    <span>Gestion de usuarios</span>
                </div>
            </div>
            <div class="col-xs-12 col-sm-12  col-md-6">
                <div class="row">
                    <div class="col-xs-12 col-sm-8  col-md-8">
                        <div id="custom-search-input">
                            <div class="input-group col-md-12">
                                <input type="text" class="  search-query form-control" placeholder="Buscar" id="Buscartxt" />
                                <span class="input-group-btn">
                                    <button class="btn btn-danger" type="button">
                                        <span class=" glyphicon glyphicon-search"></span>
                                    </button>
                                </span>
                            </div>
                        </div>
                    </div>

                    <div class="col-xs-12 col-sm-4 col-md-4 nuevoUser">
                        <button type="button" class="btn btn-success" data-toggle="modal" data-target="#ModalCrearUsuario">Nuevo usuario</button>
                    </div>
                </div>
            </div>
        </div>
        <table id="mytable" class="table table-responsive table-bordred table-striped">

            <thead>
                <th>Nombres</th>
                <th>Apellido</th>
                <th>Usuario</th>
                <th>CCF</th>
                <th>Tipo de Usuario</th>
                <th>Estado</th>
                <th></th>
                <th></th>
            </thead>
            <tbody id="tbodyGestionUsuarios">

            </tbody>

        </table>
        <br />
        <br />
        <br />
        <br />
    </form>
    <div class="modal fade" id="ModalCrearUsuario" role="dialog">
        <div class="modal-dialog modal-lg">
            <!-- Modal content-->
            <div class="modal-content">
			<!--
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Formulario</h4>
                </div>
				-->
                <div class="modal-body">

                    <div class="row">
                        <div class="col-sm-1"></div>
                        <div class="col-sm-10">
                            <div class="panel panel-primary">
                                <form class="form-horizontal">
                                    <div class="panel-heading">Nuevo Usuario</div>
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
                                                        <input type='text' class="form-control styleinput" datepicker-popup="yyyy/mm/dd" name="txtFechadecaducidad" id="txtFechadecaducidad" placeholder="YYYY-MM-DD" datepicker required readonly />
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
                        <div class="col-sm-1"></div>
                    </div>
                    <div class="group">
						<div class="col-md-3"></div>
						<div class="col-md-2">
							<button type="button" class="btn btn-primary hoverbtn" data-dismiss="modal">Cancelar</button>
						</div>						
                        <div class="col-md-2"></div>
                        <div class="col-md-2">
                            <button type="button" class="btn btn-primary hoverbtn" onclick="ValidaUsuario();">Guardar</button>
                        </div>
						<div class="col-md-3"></div>
                    </div>
                </div>
                <div class="modal-footer">
                    <div class="col-md-9"></div>
                    <div class="col-md-3">
                    </div>
                </div>
            </div>

        </div>
    </div>


    <div class="modal fade" id="ModalEditarUsuario" role="dialog">
        <div class="modal-dialog modal-lg">
            <!-- Modal content-->
            <div class="modal-content">
			<!--
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Formulario</h4>
                </div>
			-->
                <div class="modal-body">

                    <div class="row">
                        <div class="col-sm-1"></div>
                        <div class="col-sm-10">
                            <div class="panel panel-primary">
                                <form class="form-horizontal">
                                    <div class="panel-heading">Editar Usario</div>
                                    <div class="panel-body">
                                        <div class="form-horizontal">
                                            <div class="form-group">
                                                <label class="col-sm-4 control-label styleLabel">Nombres:</label>
                                                <div class="col-sm-8">
                                                    <input class="form-control styleinput" id="txtNombres2" type="text" placeholder="Nombres">
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-sm-4 control-label styleLabel">Apellidos:</label>
                                                <div class="col-sm-8">
                                                    <input class="form-control styleinput styleLabel" id="txtApellidos2" type="text" placeholder="Apellidos">
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-sm-4 control-label styleLabel">Tipo de usuario:</label>
                                                <div class="col-sm-8">
                                                    <select class="form-control styleinput" name="cboTipodeusuario2" id="cboTipodeusuario2">
                                                    </select>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-sm-4 control-label styleLabel">Fecha de caducidad:</label>
                                                <div class="col-sm-8">
                                                    <div class='input-group date' id='Div1'>
                                                        <input type='text' class="form-control styleinput" datepicker-popup="yyyy/mm/dd" name="txtFechadecaducidad2" id="txtFechadecaducidad2" placeholder="YYYY-MM-DD" datepicker required readonly />
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
                        <div class="col-sm-1"></div>
                    </div>
                    <div class="group">
						<div class="col-md-3"></div>
						<div class="col-md-2">
							<button type="button" class="btn btn-primary hoverbtn" data-dismiss="modal">Cancelar</button>
						</div>						
                        <div class="col-md-2"></div>
                        <div class="col-md-2">
                            <button type="button" class="btn btn-primary hoverbtn" onclick="ActualizarUsuario();">Actualizar</button>
                        </div>
						<div class="col-md-3"></div>
                    </div>
					<!--
                    <div class="row">
                        <div class="col-sm-9"></div>
                        <div class="col-sm-2">
                            <button type="button" class="btn btn-primary" onclick="ActualizarUsuario();">Actualizar</button>
                        </div>
                    </div>
					-->
                </div>
                <div class="modal-footer">
                    <div class="col-md-9"></div>
                    <div class="col-md-3">
                    </div>
                </div>
            </div>

        </div>
    </div>

</asp:Content>

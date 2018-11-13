<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/AllPages/pages.Master" AutoEventWireup="true" CodeBehind="Log_Consultas.aspx.cs" Inherits="Asocajas.Pages.AllPages.Log_Consultas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../Scripts/css/Login/tables/bootstrap-table.min.css" rel="stylesheet" />
    <link href="../../Scripts/css/Login/tables/bootstrap-editable.css" rel="stylesheet" />

    <script src="../../Scripts/Scripts/tables/bootstrap-table.js"></script>
    <script src="../../Scripts/Scripts/tables/bootstrap-table-editable.js"></script>
    <script src="../../Scripts/Scripts/tables/bootstrap-table-export.js"></script>
    <script src="../../Scripts/Scripts/tables/tableExport.js"></script>
    <script src="../../Scripts/Scripts/tables/bootstrap-table-filter-control.js"></script>
    <script src="../../Scripts/Javascript/log_Consultas_Ani.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TitlePage" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="bodyContent" runat="server">

    <h4 class="text-center "><b>Consulta Transacciones</b></h4>
    <form>

        <div class="encabezadoTable">
            <div class="col-xs-12 col-sm-12 col-md-12">
                <div class="col-xs-12 col-sm-12 col-md-10 ">
                    <div class="col-xs-6 col-sm-6 col-md-3 ">
                        <div class="col-xs-12 col-sm-12 col-md-12 txtEncabezadoLog">
                            <span>CCF</span>
                        </div>
                        <div class="col-xs-12 col-sm-12 col-md-12 ">
                            <select class="form-control" id="cboCCF">
                            </select>
                        </div>
                    </div>
                    <div class="col-xs-6 col-sm-6 col-md-3 ">
                        <div class="col-xs-12 col-sm-12 col-md-12 txtEncabezadoLog">
                            <span>USUARIO</span>
                        </div>
                        <div class="col-xs-12 col-sm-12 col-md-12 ">
                            <select class="form-control" id="cboUsuario">
                                <option value=""></option>
                            </select>
                        </div>
                    </div>
                    <div class="col-xs-6 col-sm-6 col-md-3 ">
                        <div class="col-xs-12 col-sm-12 col-md-12 txtEncabezadoLog">
                            <span>FECHA INICIAL</span>
                        </div>
                        <div class="col-xs-12 col-sm-12 col-md-12 ">
                            <input type='text' class="form-control" name="txtFechaIncial" id="txtFechaIncial" placeholder="YYYY-MM-DD" required readonly />
                        </div>
                    </div>
                    <div class="col-xs-6 col-sm-6 col-md-3 ">
                        <div class="col-xs-12 col-sm-12 col-md-12 txtEncabezadoLog">
                            <span>FECHA FINAL</span>
                        </div>
                        <div class="col-xs-12 col-sm-12 col-md-12 ">
                            <input type='text' class="form-control" name="txtFechaFinal" id="txtFechaFinal" placeholder="YYYY-MM-DD" required readonly />
                        </div>
                    </div>

                </div>

                <div class="col-xs-4 offset-xs-4 col-sm-4 offset-sm-4 col-md-2 offset-md-0 nuevoUser">
                    <span></span>
                    <button type="button" class="btn btn-success" onclick="Buscar();">Consultar</button>
                </div>

            </div>
        </div>


        <div class="table table-responsive Autoheight" style="display: none;" id="dvLogConsultas_Ani">
            <div id="toolbar">
            </div>
            <table id="tblLogConsultas_Ani" style="width: 100%">
                <thead>
                    <tr>
                        <th>CCF</th>
                        <th>Usuario</th>
                        <th>Id.</th>                        
						<%--<th>Documento</th>--%>
                        <th>Vía</th>                        
                        <th>Dirección IP</th>
                        <th>Fecha Transacción</th>
						<th>Resultado</th>
                    </tr>
                </thead>
            </table>
        </div>
        <br />
        <br />
        <br />
        <br />
    </form>
</asp:Content>

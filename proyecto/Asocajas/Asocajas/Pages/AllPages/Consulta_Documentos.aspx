<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/AllPages/pages.Master" AutoEventWireup="true" CodeBehind="Consulta_Documentos.aspx.cs" Inherits="Asocajas.Pages.AllPages.Consulta_Documentos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TitlePage" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="bodyContent" runat="server">
    <div class="row">
        <div class="col-sm-3">
             <h4><strong>Consulta Documentos</strong></h4>
        </div>
        <div class="col-sm-1"></div>
        <div class="col-sm-4">
            <div class="input-group">
            <span class="input-group-addon"><b>Identificación</b></span>
            <input id="Number1" type="number" class="form-control">
        </div>
        </div>
       
        <div class="col-sm-2"></div>
        <div class="col-sm-2">
            <button type="button" class="btn btn-primary">Consultar</button>
        </div>

     
    </div>
</asp:Content>

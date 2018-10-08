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

    <br/>
    <br/>

    <div class="row">
         <div class="col-sm-2"></div>
        
        <div class="col-sm-8">
    <table class="table table-bordered bordertable">

        <tbody class="bordertable">
            <tr>
                <th scope="row" class="thVertical">Identificación</th>
                <td>123456789</td>
            </tr>

            <tr>
                <th scope="row" class="thVertical">Primer Apellido</th>
                <td>Suarez</td>
            </tr>

            <tr>
                <th scope="row" class="thVertical">Particula</th>
                <td>Sin Particula</td>
            </tr>

            <tr>
                <th scope="row" class="thVertical">Segundo Apellido</th>
                <td>Gamboa</td>
            </tr>

            <tr>
                <th scope="row" class="thVertical">Primer Nombre</th>
                <td>Adriana</td>
            </tr>

            <tr>
                <th scope="row" class="thVertical">Segundo Nombre</th>
                <td>Marcela</td>
            </tr>

            <tr>
                <th scope="row" class="thVertical">Fecha de Nacimiento</th>
                <td>10/06/1980</td>
            </tr>

            <tr>
                <th scope="row" class="thVertical">Municipio de Expedición</th>
                <td>Bogotá D.C.</td>
            </tr>

            <tr>
                <th scope="row" class="thVertical">Departamento de Expedición</th>
                <td>Distrito Capital</td>
            </tr>

            <tr>
                <th scope="row" class="thVertical">Fecha de Expedición</th>
                <td>06/08/1998</td>
            </tr>

            <tr>
                <th scope="row" class="thVertical">Estado del documento</th>
                <td>0</td>
            </tr>

            <tr>
                <th scope="row" class="thVertical">Número de Resolución</th>
                <td>2355</td>
            </tr>

            <tr>
                <th scope="row" class="thVertical">Año de Resolución</th>
                <td>2007</td>
            </tr>
        </tbody>
    </table>
            </div>
        </div>
  
    <div class="row">
        <div class="col-sm-10" ></div>
        <div class="col-sm-2" >
        <button type="button" class="btn btn-primary" onclick="">Regresar</button>
            </div>
    </div>
    
   

</asp:Content>

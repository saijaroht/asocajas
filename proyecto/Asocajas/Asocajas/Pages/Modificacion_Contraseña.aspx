﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Login.Master" AutoEventWireup="true" CodeBehind="Modificacion_Contraseña.aspx.cs" Inherits="Asocajas.Pages.Modificacion_Contraseña" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TitlePage" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentBody" runat="server">

    <div class="row">
        <div class="col-md-3"></div>
        <div class="col-md-6">
        <p>Desde esta pantalla podrá solicitar la modificacion de su contraseña</p>
        <p>Introduciendo el loguin de usuario se le enviara un mensaje a su cuenta de correo electronico</p>

            <div class="group">
                <i class="fas fa-user"></i>
                <input type="text" class="form-control" placeholder="Usuario" required="required" />
            </div>
            <br />
            <br />
            <br />
            <br />

            <div class="row">
                <div class="col-md-3">
                    <button type="button" class="btn btn-primary" onclick="">Volver</button>
                </div>

                 <div class="col-md-6">
                  </div>
                <div class="col-md-3">
                    <button type="button" class="btn btn-primary" onclick="">Solicitar</button>
                </div>
            </div>
    </div>
        </div>
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />



</asp:Content>

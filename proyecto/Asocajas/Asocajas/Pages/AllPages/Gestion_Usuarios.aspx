<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/AllPages/pages.Master" AutoEventWireup="true" CodeBehind="Gestion_Usuarios.aspx.cs" Inherits="Asocajas.Pages.AllPages.Gestion_Usuarios" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../../Scripts/Javascript/Gestion_Usuarios.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TitlePage" runat="server">
    
</asp:Content>
 
<asp:Content ID="Content3" ContentPlaceHolderID="bodyContent" runat="server">
    <ul class="breadcrumb">
        <li><a href="Inicio.aspx">Administración</a></li>
        <li class="active">Gestión de Usuarios</li>
    </ul>
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
                    <input type="text" id="myInput" class="search-query form-control" placeholder="Search" />
                    <span class="input-group-btn">
                        <button class="btn btn-danger" type="button">
                            <span class=" glyphicon glyphicon-search"></span>
                        </button>
                    </span>
                </div>
            </div>
            </div>

                <div class="col-xs-12 col-sm-4 col-md-4 nuevoUser">
                <button type="button" class="btn btn-success" onclick="nuevoUsuario()">Nuevo usuario</button>
            </div>
          </div>
        </div>   
     </div> 
 
       <table id="mytable" class="table table-responsive table-bordred table-striped">
                   
                   <thead>
                   
                <th><input type="checkbox" id="checkall" /></th>
                <th>Nombres</th>
                <th>Apellido</th>
                <th>Usuario</th>
                <th>CCF</th>
                <th>Tipo de Usuario</th>
                <th>Estado</th>  
                <th></th>                      
                <th></th>
                <th></th>
                   </thead>
    <tbody id="tbodyGestionUsuarios">
    
  <%--  <tr>
    <td><input type="checkbox" class="checkthis" /></td>
    <td>Mohsin</td>
    <td>Irshad</td>
    <td>CB 106/107 Street # 11 Wah Cantt Islamabad Pakistan</td>
    <td>isometric.mohsin@gmail.com</td>
    <td>+923335586757</td>
        <td>Activo</td>
    <td><p data-placement="top" data-toggle="tooltip" title="Edit"><button class="btn btn-primary btn-xs"><span class="glyphicon glyphicon-pencil"></span></button></p></td>
    <td><p data-placement="top" data-toggle="tooltip" title="Delete"><button class="btn btn-danger btn-xs"><span class="glyphicon glyphicon-trash"></span></button></p></td>
    <td><p data-placement="top" data-toggle="tooltip" title="open"><button class="btn btn-success btn-xs"><span class="glyphicon glyphicon-eye-open"></span></button></p></td>
    <td class="hidden"><p data-placement="top" data-toggle="tooltip" title="close"><button class="btn btn-danger btn-xs"><span class="glyphicon glyphicon-eye-close"></span></button></p></td>
    </tr>
    
 <tr>
    <td><input type="checkbox" class="checkthis" /></td>
    <td>Mohsin</td>
    <td>Irshad</td>
    <td>CB 106/107 Street # 11 Wah Cantt Islamabad Pakistan</td>
    <td>isometric.mohsin@gmail.com</td>
    <td>+923335586757</td>
     <td>Activo</td>
    <td><p data-placement="top" data-toggle="tooltip" title="Edit"><button class="btn btn-primary btn-xs" ><span class="glyphicon glyphicon-pencil"></span></button></p></td>
    <td><p data-placement="top" data-toggle="tooltip" title="Delete"><button class="btn btn-danger btn-xs"><span class="glyphicon glyphicon-trash"></span></button></p></td>
    <td class="hidden"><p data-placement="top" data-toggle="tooltip" title="open"><button class="btn btn-success btn-xs"><span class="glyphicon glyphicon-eye-open"></span></button></p></td>
     <td><p data-placement="top" data-toggle="tooltip" title="closes"><button class="btn btn-danger btn-xs"><span class="glyphicon glyphicon-eye-close"></span></button></p></td>
 </tr>

        <tr>
    <td><input type="checkbox" class="checkthis" /></td>
    <td>Cristhian Camilo</td>
    <td>Gutierrez</td>
    <td>CB 106/107 Street # 11 Wah Cantt Islamabad Pakistan</td>
    <td>usuario@gmail.com</td>
    <td>+923335586757</td>
     <td>Activo</td>
    <td><p data-placement="top" data-toggle="tooltip" title="Edit"><button class="btn btn-primary btn-xs" ><span class="glyphicon glyphicon-pencil"></span></button></p></td>
    <td><p data-placement="top" data-toggle="tooltip" title="Delete"><button class="btn btn-danger btn-xs"><span class="glyphicon glyphicon-trash"></span></button></p></td>
    <td class="hidden"><p data-placement="top" data-toggle="tooltip" title="open"><button class="btn btn-success btn-xs"><span class="glyphicon glyphicon-eye-open"></span></button></p></td>
     <td><p data-placement="top" data-toggle="tooltip" title="closes"><button class="btn btn-danger btn-xs"><span class="glyphicon glyphicon-eye-close"></span></button></p></td>
 </tr>
    --%>
        
    </tbody>
        
    </table>
</form>
      
</asp:Content>

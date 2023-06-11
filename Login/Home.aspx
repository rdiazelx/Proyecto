<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="Login.Home" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Style/Style.css" rel="stylesheet" />

</head>


<body>



    <h2>Agregar Persona</h2>


   <div class="container" id="myContainer">

    <form runat="server" id="formPersona">

        <asp:TextBox ID="txtNombre" runat="server" CssClass="estandarTexbox" placeholder="nombre"></asp:TextBox>

        <asp:TextBox ID="txtApellido1" runat="server" CssClass="estandarTexbox" placeholder="Apellido1"></asp:TextBox>

        <asp:TextBox ID="txtApellido2" runat="server" CssClass="estandarTexbox" placeholder="Apellido2"></asp:TextBox>

        <asp:DropDownList ID="dptipoIdentificacion" CssClass="dropdownClass" runat="server"></asp:DropDownList>

        <asp:TextBox ID="txtIdentifiacion" runat="server" CssClass="estandarTexbox" placeholder="Identificacion"></asp:TextBox>

        <asp:TextBox ID="txtFechaNacimiento" runat="server" CssClass="estandarTexbox" placeholder="dd/mm/aaaa" TextMode="Date"></asp:TextBox>

        <asp:DropDownList ID="dpEstadoCivil" runat="server" CssClass="dropdownClass"></asp:DropDownList>

        <asp:DropDownList ID="dpGenero" runat="server" CssClass="dropdownClass"></asp:DropDownList>

        <p>
            <asp:Button ID="btnGuardar" runat="server" Text="Guardar" OnClick="btnGuardar_Click" CssClass="btnClass" />
        </p>
        

    </form>

       <a href="listaPersonas.aspx" runat="server" style="display: none;" id="linkListaPersonas" target="_blank">Ver lista Personas</a>


</div>


    <div class="dialog-container" id="divMensaje" style="display: none;" runat="server">
        <div class="message-box">
            <div id="mensajeContenido">
                <span id="mensajeTexto" runat="server"></span>
                <button id="cerrarMensaje" class="btnClass btnMensaje" onclick="cerrarMensaje()">Cerrar</button>

            </div>
        </div>
    </div>
     <script>
        function cerrarMensaje() {
            var divMensaje = document.getElementById("divMensaje");
            divMensaje.style.display = "none";
        }
     </script>
 


</body>
</html>

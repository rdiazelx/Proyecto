<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="listaPersonas.aspx.cs" Inherits="Login.listaPersonas" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Lista personas</title>
    <link href="Style/Style.css" rel="stylesheet" />
</head>
<body>
    <h2>Consultar lista de personas</h2>

    <div class="containerTable">
        <form id="formListaPersonas" runat="server">
            <div>
                <asp:TextBox ID="txtFiltro" runat="server" placeholder="Filtro por genero"></asp:TextBox>
                <asp:Button ID="btnFiltar" runat="server" Text="Filtrar" OnClick="btnFiltar_Click" />
            </div>
            
            
            <div>

                <asp:GridView ID="gridListaPersonas" runat="server"></asp:GridView>


            </div>
        </form>

        <div class="dialog-container" id="divMensaje" style="display: none;" runat="server">
            <div class="message-box">
                <div id="mensajeContenido">
                    <span id="mensajeTexto" runat="server"></span>
                    <button id="cerrarMensaje" class="btnClass btnMensaje" onclick="">regresar</button>
                </div>
            </div>
        </div>
</body>
</html>

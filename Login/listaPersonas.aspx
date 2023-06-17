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



        <div class="container-botones">

            <div class="divLeft">
                <h2>Descarga</h2>
                <asp:Button ID="btnDescargar" runat="server" Text="TXT" CssClass="btnClass" OnClick="btnDescargar_Click" />
                <asp:Button ID="ButbtnDescargaXML" runat="server" Text="XML" CssClass="btnClass" OnClick="ButbtnDescargaXML_Click" />
                <asp:Button ID="btnDescargarExcel" runat="server" Text="Excel" CssClass="btnClass" OnClick="btnDescargarExcel_Click" />
            </div>

            <div class="divRight">
                <h2>Importar</h2>
                <asp:FileUpload ID="FileUpload1" runat="server" />
                <asp:Button ID="btnImportar" runat="server" Text="Importar" CssClass="btnClass" OnClick="btnImportar_Click" />
                <asp:Button ID="btnCargaXML" runat="server" Text="XML" CssClass="btnClass" OnClick="btnCargaXML_Click" />
                <asp:Button ID="btnCargaExcel" runat="server" Text="Documento de Excel" CssClass="btnClass" OnClick="btnCargaExcel_Click" />
            </div>
        </div>
        </div>

      



    </form>
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

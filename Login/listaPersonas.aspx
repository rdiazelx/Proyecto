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

                <asp:GridView ID="gridListaPersonas" runat="server"></asp:GridView>


            </div>
        </form>
    </div>






</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Login.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Style/Style.css" rel="stylesheet" />
</head>
<body>
    <%--comentatio para betty--%>

     <div class="login-container">
   
    <form id="form1" runat="server">
        <div>
            <table class="center-element text-center tablaLogin">
                <tr>
                    <td colspan="2">
                        <h1>Inicio</h1>
                    </td>
                </tr>
                <tr>
                  

                    <td>
                        <asp:TextBox ID="txtLogin" runat="server" CssClass=".standardTextbox" placeholder="Usuario"></asp:TextBox>
                    </td>
                </tr>

                 <tr>
                 

                    <td>
                        <asp:TextBox ID="txtPassword" runat="server" CssClass=".standardTextbox" placeholder="Password"></asp:TextBox>
                    </td>
                </tr>

                <tr>
                    <td colspan="2">
                         <br />
                         <asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click" />
                        <br />
                        <asp:Label ID="lblMensaje" runat="server" Visible="False"></asp:Label>
                    </td>
                </tr>
               
            
            </table>






        </div>
    </form>
         </div>
</body>
</html>

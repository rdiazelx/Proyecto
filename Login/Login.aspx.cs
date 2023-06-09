using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Login
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {

            string User = txtLogin.Text;
            string password = txtPassword.Text;

            if (User == "admin" && password == "admin") {
                Response.Redirect("/Home.aspx");
            }
            else{
                lblMensaje.Text = "Usuario o contraseña incorrecto";
                lblMensaje.Visible = true;
            }
        }



    }
}
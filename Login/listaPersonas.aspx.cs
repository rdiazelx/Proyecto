using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Login
{
    public partial class listaPersonas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private void CargaTablaPersonas()
        {
            try
            {

                var listaPersona = (List<oPersona>)Session["listaPersona"];

                if (listaPersona != null)
                {


                }

            }
            catch (Exception)
            {

                throw;
            }


        }







    }
}
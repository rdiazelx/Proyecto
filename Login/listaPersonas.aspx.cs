using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
            CargaTablaPersonas();
        }

        private void CargaTablaPersonas()
        {
            try
            {

                var listaPersonas = (List<oPersona>)Session["listaPersona"];

                if (listaPersonas == null)
                {
                    mensajeTexto.InnerText = "No hay datos que mostrar";
                    //Mostrar el cuadro de mensaje
                    divMensaje.Style["display"] = "block";

                }
                else
                {
                    var dt= GeneraTablaDinamica<oPersona>(listaPersonas);

                    gridListaPersonas.DataSource = dt;
                    gridListaPersonas.DataBind();


                }

            }
            catch (Exception)
            {

                throw;
            }


        }

        private DataTable GeneraTablaDinamica<T>(List<T> lista)
        {
            DataTable dt = new DataTable();

            PropertyDescriptorCollection listaProp = TypeDescriptor.GetProperties(typeof(T));


            for (int i = 0; i < listaProp.Count; i++)
            {
                PropertyDescriptor prop = listaProp[i];
                dt.Columns.Add(prop.Name, prop.PropertyType);

            }

            object[] valores = new object[listaProp.Count];

            foreach (T item in lista) 
            {
                for (int i = 0; i < valores.Length; i++)
                {
                    valores[i] = listaProp[i].GetValue(item);
                }
                dt.Rows.Add(valores);
            }
            return dt;

        }

        protected void btnFiltar_Click(object sender, EventArgs e)
        {
            try
            {

                string varFiltro = txtFiltro.Text.ToLower();

                var listaPersonas = (List<oPersona>)Session["ListaPersona"];


                if (!string.IsNullOrEmpty(varFiltro))
                {
                    listaPersonas = listaPersonas.FindAll(p => p.genero.ToLower().Contains(varFiltro));

                    //=> quiere decir donde

                }
                             
                    var dt = GeneraTablaDinamica<oPersona>(listaPersonas);

                    gridListaPersonas.DataSource = dt;
                    gridListaPersonas.DataBind();

              

            }
            catch (Exception ex)
            {

                mensajeTexto.InnerText = "Ocurrio un error. (Error: " + ex.Message + ")";
                //Mostrar el cuadro de mensaje
                divMensaje.Style["display"] = "block";
            }
        }
    }
}
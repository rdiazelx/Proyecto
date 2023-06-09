using Login;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Login
{
    public partial class Home : System.Web.UI.Page
    {

        //instanciar listas utilizan listas
        List<oTipoIdentifiacion> listaTipoIdentifiacion = new List<oTipoIdentifiacion>();
        List<oGenero> listaGenero = new List<oGenero>();


        //crear tablas
        DataTable dtEstadoCivil = new DataTable("dtEstadoCivil");




        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {



                //define la estructura de la tabla
                dtEstadoCivil.Columns.Add("ID");
                dtEstadoCivil.Columns.Add("estadoCivil");

                //llena la tabla
                dtEstadoCivil.Rows.Add(1, "Soltero");
                dtEstadoCivil.Rows.Add(2, "Casado");
                dtEstadoCivil.Rows.Add(3, "Viudo");

                //asignar al dropdown
                dpEstadoCivil.DataTextField = "estadoCivil";
                dpEstadoCivil.DataValueField = "ID";


                dpEstadoCivil.DataSource = dtEstadoCivil;
                dpEstadoCivil.DataBind();

                //Llena lista tipo de identifiacion
                listaTipoIdentifiacion.Add(new oTipoIdentifiacion { id = 1, tipoIdentificacion = "Cedula" });
                listaTipoIdentifiacion.Add(new oTipoIdentifiacion { id = 2, tipoIdentificacion = "Dimex" });
                listaTipoIdentifiacion.Add(new oTipoIdentifiacion { id = 3, tipoIdentificacion = "Pasaporte" });

                foreach (var item in listaTipoIdentifiacion)
                {
                    dptipoIdentificacion.Items.Add(new ListItem(item.tipoIdentificacion, item.id.ToString()));
                }

                //Llenar lista de genero

                listaGenero.Add(new oGenero { id = 1, genero = "Hombre" });
                listaGenero.Add(new oGenero { id = 2, genero = "Mujer" });
                listaGenero.Add(new oGenero { id = 3, genero = "Otro" });

                foreach (var item in listaGenero)
                {
                    dpGenero.Items.Add(new ListItem(item.genero, item.id.ToString()));

                }
            }
        }


        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try {
                string nombre = txtNombre.Text;
                string apellido1 = txtApellido1.Text;
                string apellido2= txtApellido2.Text;
                string identificacion = txtIdentifiacion.Text;
                string fechadeNacimiento = txtFechaNacimiento.Text;
                string estadoCivil = dpEstadoCivil.SelectedItem.Text;
                string tipoIdentificacion = dptipoIdentificacion.SelectedItem.Text;
                string genero = dpGenero.SelectedItem.Text;

                if (!string.IsNullOrEmpty(nombre) || !string.IsNullOrEmpty(identificacion))
                {

                //definir la lista
                List<oPersona> listaPersona = new List<oPersona>();
                    
                 var objPersona = new oPersona();

                    objPersona.nombre = nombre;
                    objPersona.apellido1 = apellido1;
                    objPersona.apellido2 = apellido2;
                    objPersona.identificacion = identificacion;
                    objPersona.fechaDeNacimiento = DateTime.Parse(fechadeNacimiento);
                    objPersona.estadoCivil= estadoCivil;
                    objPersona.tipoIdentificacion= tipoIdentificacion;
                    objPersona.genero = genero;

                    //agrega el objeto a la lista
                    listaPersona.Add(objPersona);

                    //Guardamos la lista como sesion

                    Session["listapersona"] = listaPersona;




                }
                else
                {
                    mensajeTexto.InnerText = "El nombre o la identificacion no puede estar vacio";

                    //Mostrar el cuadro de mensaje
                    divMensaje.Style["display"] = "block";
                   
                }


            } catch (Exception ex) { 
            //Texto del mensaje
            
             
            mensajeTexto.InnerText = "Ocurrio un error. (Error: " + ex.Message + ")";

                //Mostrar el cuadro de mensaje
                divMensaje.Style["display"] = "block";
            

            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
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

        protected void btnImportar_Click(object sender, EventArgs e)
        {
            try
            {
                //preguntar si el fileupload tiene un archivo

                if (FileUpload1.HasFile)
                {
                    string nombreArchivo = Path.GetFileName(FileUpload1.PostedFile.FileName);
                    string tipoExtension = Path.GetExtension(FileUpload1.PostedFile.FileName);
                    string random = DateTime.Now.ToFileTime().ToString();
                    string folder = "~/Uploads";

                    //definimos la ruta completa del archivo que se va a cargar

                    string filepath = Server.MapPath(folder + random + nombreArchivo);

                    FileUpload1.SaveAs(filepath);

                    //leer el archivo

                    string[] lineas= File.ReadAllLines(filepath);

                    //recorrer el archivo para cargarlo en la lista y en la sesion

                    if (lineas.Length > 0)
                    {

                    var listaPersonas = new List<oPersona>();

                        foreach (string  line in lineas)
                        {
                            string[] linea = line.Split(';');

                            var objPersona = new oPersona();

                            objPersona.nombre = linea[0];
                            objPersona.apellido1 = linea[1];
                            objPersona.apellido2 = linea[2];
                            objPersona.identificacion= linea[3];
                            objPersona.tipoIdentificacion = linea[4];
                            objPersona.fechaDeNacimiento = DateTime.Parse(linea[5]);
                            objPersona.estadoCivil = linea[6];
                            objPersona.genero = linea[7];

                            listaPersonas.Add(objPersona);
                        }

                        Session["listaPersona"] = listaPersonas;

                        //genera la tabla dinamica
                        var dt = GeneraTablaDinamica<oPersona>(listaPersonas);

                        gridListaPersonas.DataSource = dt;
                        gridListaPersonas.DataBind();

                    }

                }
                else
                {
                    mensajeTexto.InnerText = "Debe seleccionar un archivo";
                    //Mostrar el cuadro de mensaje
                    divMensaje.Style["display"] = "block";
                }

            }
            catch (Exception ex)
            {
                mensajeTexto.InnerText = "Ocurrio un error. (Error: " + ex.Message + ")";
                //Mostrar el cuadro de mensaje
                divMensaje.Style["display"] = "block";

            }
        }

        protected void btnDescargar_Click(object sender, EventArgs e)
        {

            try
            {

                var listaPersona = (List<oPersona>)Session["listaPersona"];

                //definir una variable texto

                string archivoTexto = string.Empty;
                
                
                if (listaPersona!=null)
                {
                   
                    //recorremos la lista para generar el texto para enviar al archivo
                    foreach (var item in listaPersona)
                    {
                        archivoTexto += item.nombre + ";" + item.apellido1 + ";" + item.apellido2 + ";" + item.identificacion + ";" + item.tipoIdentificacion + ";" + item.fechaDeNacimiento;

                        archivoTexto += "\r\n";
                        
                    }
                    string nombreArchivo = "Personas.txt";

                    Response.Clear();
                    Response.Buffer = true;
                    Response.AddHeader("Content-disposition", "attachment;filename=" + nombreArchivo);
                    Response.ContentType ="application/text";
                    Response.Output.Write(archivoTexto);
                    Response.Flush();
                    Response.End();
                }
            }
            catch (Exception ex)
            {
                mensajeTexto.InnerText = "Ocurrio un error. (Error: " + ex.Message + ")";
                //Mostrar el cuadro de mensaje
                divMensaje.Style["display"] = "block";

            }

        }


        protected void ButbtnDescargaXML_Click(object sender, EventArgs e)
        {

            try
            {
                //Obtener la lista de la sesion
                var listaPersona = (List<oPersona>)Session["listaPersona"];

                if (listaPersona != null)
                {
                    var dt = GeneraTablaDinamica<oPersona>(listaPersona);
                    dt.TableName = "Persona";

                    DataSet ds= new DataSet("ListaPersonas");
                    ds.Tables.Add(dt);

                    //configurar la ruta para guardar el archivo de forma temporal en el servidor antes de descargar.

                    string random = DateTime.Now.ToFileTime().ToString();
                    string folder = "~/Uploads";
                    String ruta = Server.MapPath(folder + random + "archivo.xml");

                    //escribir el archivo XML
                    ds.WriteXml(ruta);

                    //Devolver el archivo al cliente
                    Response.Clear();
                    Response.AppendHeader("Content-disposition", "application;filename=Personas.xml");
                    // Set the content type
                    Response.ContentType = "application/xml";
                    Response.WriteFile(ruta);
                    Response.End();

                }
                else
                {
                    // Establecer el texto del mensaje
                    mensajeTexto.InnerText = "No hay datos para descargar";
                    // Mostrar el cuadro de mensaje
                    divMensaje.Style["display"] = "block";
                }
            }
            catch (Exception ex)
            {
                // Establecer el texto del mensaje
                mensajeTexto.InnerText = "Ocurrió un error. (Error: " + ex.Message + ")";
                // Mostrar el cuadro de mensaje
                divMensaje.Style["display"] = "block";
            }

        }

        protected void btnCargaXML_Click(object sender, EventArgs e)
        {
            try
            {
                //preguntar si el fileupload tiene un archivo cargado
                if (FileUpload1.HasFile)
                {
                    using (DataSet ds = new DataSet())
                    {
                        ds.ReadXml(FileUpload1.PostedFile.InputStream);
                        var dt = ds.Tables[0];

                        //puede mostrar en el gridView
                        gridListaPersonas.DataSource = dt;
                        gridListaPersonas.DataBind();

                        //recorrer la tabla para generar la lista
                        var listaPersonas = new List<oPersona>();

                        if (dt.Rows.Count > 0)
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                var objPersona = new oPersona();
                                objPersona.nombre = dt.Rows[i]["nombre"].ToString();
                                objPersona.apellido1 = dt.Rows[i]["apellido1"].ToString();
                                objPersona.apellido2 = dt.Rows[i]["apellido2"].ToString();
                                objPersona.identificacion = dt.Rows[i]["identificacion"].ToString();
                                objPersona.tipoIdentificacion = dt.Rows[i]["tipoIdentificacion"].ToString();
                                objPersona.fechaDeNacimiento = DateTime.Parse(dt.Rows[i]["fechaDeNacimiento"].ToString());
                                objPersona.estadoCivil = dt.Rows[i]["estadoCivil"].ToString();
                                objPersona.genero = dt.Rows[i]["genero"].ToString();

                                listaPersonas.Add(objPersona);

                            }

                            //agregarlo a la session
                            Session["listaPersona"] = listaPersonas;
                        }
                    }
                }
                else
                {
                    // Establecer el texto del mensaje
                    mensajeTexto.InnerText = "Debe seleccionar un archivo.";
                    // Mostrar el cuadro de mensaje
                    divMensaje.Style["display"] = "block";
                }
            }
            catch (Exception ex)
            {
                // Establecer el texto del mensaje
                mensajeTexto.InnerText = "Ocurrió un error. (Error: " + ex.Message + ")";
                // Mostrar el cuadro de mensaje
                divMensaje.Style["display"] = "block";
            }
        }


        protected void btnDescargarExcel_Click(object sender, EventArgs e)
        {
            try
            {
                //obtener la lista de la session
                var listaPersona = (List<oPersona>)Session["listaPersona"];
                if (listaPersona != null)
                {
                    var dt = GeneraTablaDinamica<oPersona>(listaPersona);

                    //configurar el libro de excel
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        //crear la hoja y agregar la hoja
                        wb.Worksheets.Add(dt, "ListaPersonas");

                        //Respondemos al cliente para descarga
                        Response.Clear();
                        Response.Buffer = true;
                        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        Response.AddHeader("content-disposition", "attachment;filename=Personas.xlsx");

                        using (MemoryStream ms = new MemoryStream())
                        {
                            wb.SaveAs(ms);
                            ms.WriteTo(Response.OutputStream);
                            Response.Flush();
                            Response.End();
                        }
                    }
                }
                else
                { // Establecer el texto del mensaje
                    mensajeTexto.InnerText = "No hay datos para descargar.";
                    // Mostrar el cuadro de mensaje
                    divMensaje.Style["display"] = "block";

                }
            }
            catch (Exception ex)
            {
                // Establecer el texto del mensaje
                mensajeTexto.InnerText = "Ocurrió un error. (Error: " + ex.Message + ")";
                // Mostrar el cuadro de mensaje
                divMensaje.Style["display"] = "block";
            }
        }

        protected void btnCargaExcel_Click(object sender, EventArgs e)
        {
            try
            {
                //preguntar si el fileupload tiene un archivo cargado
                if (FileUpload1.HasFile)
                {
                    //
                    //configurar el comportamiento del FileUpload
                    string nombreArchivo = Path.GetFileName(FileUpload1.PostedFile.FileName);
                    string tipoExtension = Path.GetExtension(FileUpload1.PostedFile.FileName);
                    string random = DateTime.Now.ToFileTime().ToString();
                    string folder = "~/Uploads/";

                    if (tipoExtension == ".xlsx")
                    {
                        string ruta = Server.MapPath(folder + random + nombreArchivo);
                        FileUpload1.SaveAs(ruta);

                        //leer el archivo de excel
                        string conectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 8.0;HDR={1}'";

                        conectionString = string.Format(conectionString, ruta, "Yes");

                        OleDbConnection connExcel = new OleDbConnection(conectionString);
                        OleDbCommand cmdExcel = new OleDbCommand();
                        OleDbDataAdapter adapterExcel = new OleDbDataAdapter();

                        cmdExcel.Connection = connExcel;

                        //abrir el archivo
                        connExcel.Open();

                        //si no se conoce el nombre de las hojas, se puede obtener dinamicamente
                        DataTable dtExcel = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                        string hojaExcel = dtExcel.Rows[0]["TABLE_NAME"].ToString();

                        //lectura d elos datos
                        DataTable dt = new DataTable();

                        cmdExcel.CommandText = "Select * from [" + hojaExcel + "]";
                        adapterExcel.SelectCommand = cmdExcel;
                        adapterExcel.Fill(dt);

                        //cerramos la conexion
                        connExcel.Close();

                        //puede mostrar en el gridView
                        gridListaPersonas.DataSource = dt;
                        gridListaPersonas.DataBind();

                        //recorrer la tabla para generar la lista
                        var listaPersonas = new List<oPersona>();

                        if (dt.Rows.Count > 0)
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                var objPersona = new oPersona();
                                objPersona.nombre = dt.Rows[i]["nombre"].ToString();
                                objPersona.apellido1 = dt.Rows[i]["apellido1"].ToString();
                                objPersona.apellido2 = dt.Rows[i]["apellido2"].ToString();
                                objPersona.identificacion = dt.Rows[i]["identificacion"].ToString();
                                objPersona.tipoIdentificacion = dt.Rows[i]["tipoIdentificacion"].ToString();
                                objPersona.fechaDeNacimiento = DateTime.Parse(dt.Rows[i]["fechaNacimiento"].ToString());
                                objPersona.estadoCivil = dt.Rows[i]["estadoCivil"].ToString();
                                objPersona.genero = dt.Rows[i]["genero"].ToString();

                                listaPersonas.Add(objPersona);

                            }

                            //agregarlo a la session
                            Session["listaPersona"] = listaPersonas;
                        }


                    }
                    else
                    {
                        // Establecer el texto del mensaje
                        mensajeTexto.InnerText = "Solamente se permient formatos XLSX";
                        // Mostrar el cuadro de mensaje
                        divMensaje.Style["display"] = "block";
                    }
                }
                else
                {
                    // Establecer el texto del mensaje
                    mensajeTexto.InnerText = "Debe seleccionar un archivo.";
                    // Mostrar el cuadro de mensaje
                    divMensaje.Style["display"] = "block";
                }
            }
            catch (Exception ex)
            {
                // Establecer el texto del mensaje
                mensajeTexto.InnerText = "Ocurrió un error. (Error: " + ex.Message + ")";
                // Mostrar el cuadro de mensaje
                divMensaje.Style["display"] = "block";
            }

        }














    }
}